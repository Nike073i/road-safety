using System.Globalization;
using System.Text;
using RoadSafety.BuildingBlocks.Application.Cache;
using ZiggyCreatures.Caching.Fusion;

namespace RoadSafety.BuildingBlocks.Infrastructure.Cache
{
	public class CacheService(IFusionCache cache) : ICacheService
	{
		private readonly IFusionCache _cache = cache;

		public string GenerateCacheKey<T>(T data)
		{
			var type = typeof(T);
			var properties = type.GetProperties();
			var keyBuilder = new StringBuilder(type.Name);
			foreach (var property in properties)
			{
				if (property.GetIndexParameters().Length == 0)
				{
					object? propValue = property.GetValue(data);
					string name = property.Name;
					string value = propValue?.ToString() ?? "";
					keyBuilder.Append(CultureInfo.InvariantCulture, $"_{name}:{value}");
				}
			}

			return keyBuilder.ToString();
		}

		public Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken) =>
			_cache.GetOrDefaultAsync<T>(cacheKey, token: cancellationToken).AsTask();

		public Task<T> GetOrSetAsync<T>(
			string cacheKey,
			Func<CancellationToken, Task<T>> factory,
			TimeSpan? expiration,
			CancellationToken cancellationToken
		) =>
			_cache
				.GetOrSetAsync<T>(
					cacheKey,
					factory,
					token: cancellationToken,
					options: Create(expiration)
				)
				.AsTask();

		public Task RemoveAsync(string cacheKey, CancellationToken cancellationToken) =>
			_cache.RemoveAsync(cacheKey, token: cancellationToken).AsTask();

		public Task SetAsync<T>(
			string cacheKey,
			T value,
			TimeSpan? expiration,
			CancellationToken cancellationToken
		) => _cache.SetAsync(cacheKey, value, Create(expiration), cancellationToken).AsTask();

		private static FusionCacheEntryOptions DefaultOptions =>
			new() { Duration = TimeSpan.FromMinutes(1) };

		private static FusionCacheEntryOptions Create(TimeSpan? expiration) =>
			expiration.HasValue
				? new FusionCacheEntryOptions { Duration = expiration.Value }
				: DefaultOptions;
	}
}
