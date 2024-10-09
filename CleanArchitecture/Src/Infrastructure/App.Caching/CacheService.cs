using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching
{
	public class CacheService(IMemoryCache memoryCache) : ICacheService
	{
		public Task AddAsync<T>(string cacheKey, T value, TimeSpan expTimeSpan)
		{
			var cacheOptions = new MemoryCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = expTimeSpan
			};

			memoryCache.Set(cacheKey, value, cacheOptions);
			return Task.CompletedTask;
		}

		public Task<T?> GetAsync<T>(string cacheKey)
		{
			if (memoryCache.TryGetValue(cacheKey, out T item))
				return Task.FromResult(item);
			
			return Task.FromResult(default(T));
		}

		public Task RemoveAsync<T>(string cacheKey)
		{
			memoryCache.Remove(cacheKey);

			return Task.CompletedTask;
		}
	}
}
