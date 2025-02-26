using StackExchange.Redis;
using Newtonsoft.Json;

namespace PortofollioAPI.Services
{
    public class RedisCacheService
    {
        private readonly IDatabase _cache;

        public RedisCacheService(IConfiguration configuration)
        {
            var redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            _cache = redis.GetDatabase();
        }

        public async Task SetCacheAsync<T>(string key, T value, TimeSpan expiration)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            await _cache.StringSetAsync(key, jsonData, expiration);
        }

        public async Task<T?> GetCacheAsync<T>(string key)
        {
            var jsonData = await _cache.StringGetAsync(key);
            return jsonData.IsNullOrEmpty ? default : JsonConvert.DeserializeObject<T>(jsonData!);
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }
    }
}
