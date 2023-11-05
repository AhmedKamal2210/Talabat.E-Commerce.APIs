using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Sevices.IServices.ICacheServices;

namespace Talabat.E_Commerce1.Helpers
{
    // doing Custom Attribute For Caching
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;


        public CacheAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context , ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>(); // inject icached Service

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request); // get CacheKey From the request

            var cachedResponse = await cacheService.GetCasheResponeAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse)) // if the ke exist in redis
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;

                return;
            }

            var executedContext = await next(); // if the key dosn't in redis so will get datafrom database

            if (executedContext.Result is OkObjectResult response)
                await cacheService.SetCacheResponseAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");

            foreach ( var (key , value) in request.Query.OrderBy(x => x.Key) ) 
            {
                cacheKey.Append($"|{key} - {value}");
            }

            return cacheKey.ToString();

        }
    }
}
