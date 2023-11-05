using StackExchange.Redis;
using System.Text.Json;
using Talabat.Sevices.IServices.ICacheServices;

namespace Talabat.Sevices.Services.CasheService
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase(); // Connect to in-memmory database (redis)
        }

        public async Task SetCacheResponseAsync(string casheKey, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serializedResponse = JsonSerializer.Serialize(response, options); 

            await _database.StringSetAsync(casheKey, serializedResponse, timeToLive);
        }
        public async Task<string> GetCasheResponeAsync(string casheKey)
        {
            var cachedResponse = await _database.StringGetAsync(casheKey);

            if (cachedResponse.IsNullOrEmpty)
                return string.Empty;

            return cachedResponse.ToString();
        }
    }
}
