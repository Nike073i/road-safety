using MediatR;
using RoadSafety.BuildingBlocks.Application.Cache;

namespace RoadSafety.BuildingBlocks.QueryStack.Cache
{
	public class CachingBehavior<TRequest, TResponse>(ICacheService cacheService)
		: IPipelineBehavior<TRequest, TResponse>
		where TRequest : CacheableQuery<TResponse>
	{
		private readonly ICacheService _cacheService = cacheService;

		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken
		)
		{
			if (request.IgnoreCache)
				return await next();

			string cacheKey = _cacheService.GenerateCacheKey(request);
			var cachedResult = await _cacheService.GetAsync<TResponse>(cacheKey, cancellationToken);

			if (cachedResult is not null)
				return cachedResult;

			var result = await next();
			if (result is not null)
			{
				await _cacheService.SetAsync(
					cacheKey,
					result,
					request.ExpirationTime,
					cancellationToken
				);
			}
			return result;
		}
	}
}
