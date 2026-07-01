namespace Doccure.MarketService.Services
{
    public interface IRedisService
    {
        Task SetValueAsync(string key, string value);
        Task<string> GetValueAsync(string key);
        Task DeleteValueAsync(string key);
        Task DeleteKeyAsync(string key);

    }
}
