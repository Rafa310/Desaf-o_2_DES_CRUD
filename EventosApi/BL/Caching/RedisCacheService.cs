using StackExchange.Redis;
using System.Text.Json;

namespace EventosApi.BL.Caching
{
    public class RedisCacheService(IConnectionMultiplexer redis) : ICacheService
    {
        public async Task<T?> TryGetCacheAsync<T>(string key)
        {
            var db = redis.GetDatabase();
            var cached = await db.StringGetAsync(key);

            if (cached.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>((string)cached!);
        }

        public async Task TrySetCacheAsync<T>(string key, T value, TimeSpan duration)
        {
            var db = redis.GetDatabase();
            var json = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, json, duration);
        }

        public async Task TryDeleteCacheAsync(string key)
        {
            var db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }
}
