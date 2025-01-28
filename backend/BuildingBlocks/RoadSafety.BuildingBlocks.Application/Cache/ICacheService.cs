namespace RoadSafety.BuildingBlocks.Application.Cache
{
	public interface ICacheService
	{
		string GenerateCacheKey<T>(T data);

		Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default);

		Task<T> GetOrSetAsync<T>(
			string cacheKey,
			Func<CancellationToken, Task<T>> factory,
			TimeSpan? expiration = null,
			CancellationToken cancellationToken = default
		);

		Task SetAsync<T>(
			string cacheKey,
			T value,
			TimeSpan? expiration = null,
			CancellationToken cancellationToken = default
		);
		Task RemoveAsync(string cacheKey, CancellationToken cancellationToken = default);
	}
}
