using RoadSafety.BuildingBlocks.QueryStack.Cqrs;

namespace RoadSafety.BuildingBlocks.QueryStack.Cache
{
	public abstract record CacheableQuery<TResult> : IQuery<TResult>
	{
		public bool IgnoreCache { get; set; }

		public TimeSpan? ExpirationTime { get; set; }
	}
}
