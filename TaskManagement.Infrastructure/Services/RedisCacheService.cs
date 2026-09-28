using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache=cache;
        }

        public async Task<T?> GetAsync<T>(string Key, CancellationToken cancellationToken)
        {
            var date = await _cache.GetStringAsync(Key, cancellationToken);
            return date is null ? default : JsonSerializer.Deserialize<T>(date);

        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
             await _cache.RemoveAsync(key, cancellationToken);
        }

        public async Task SetAsync<T>(string key, T Value, TimeSpan expiration, CancellationToken cancellationToken)
        {

            var date = JsonSerializer.Serialize(Value);
            await _cache.SetStringAsync(key, date, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow  = expiration
            }, cancellationToken);
            
        }
    }
}
