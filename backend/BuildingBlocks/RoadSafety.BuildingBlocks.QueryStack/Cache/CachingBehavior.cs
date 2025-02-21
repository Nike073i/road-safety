using MediatR;
using RoadSafety.BuildingBlocks.Application.Cache;

namespace RoadSafety.BuildingBlocks.QueryStack.Cache
{
	public class CachingBehavior<TRequest, TResponse>(ICacheService cacheService)
		: IPipelineBehavior<TRequest, TResponse>
		where TRequest : CacheableQuery<TResponse>
	{
		public Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken
		)
		{
			if (request.IgnoreCache)
				return next();

			string cacheKey = cacheService.GenerateCacheKey(request);

			return cacheService.GetOrSetAsync(
				cacheKey,
				token => next(),
				request.ExpirationTime,
				cancellationToken
			);
		}
	}
}
