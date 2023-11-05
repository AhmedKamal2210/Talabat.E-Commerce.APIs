namespace Talabat.Sevices.IServices.ICacheServices
{
    public interface ICacheService
    {
        Task SetCacheResponseAsync(string casheKey, object response, TimeSpan timeToLive);
        Task<string> GetCasheResponeAsync(string casheKey);
    }
}
