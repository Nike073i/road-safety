namespace RoadSafety.BuildingBlocks.QueryStack.Cqrs
{
	public interface IQueryDispatcher
	{
		Task<TResponse> SendQuery<TResponse>(
			IQuery<TResponse> query,
			CancellationToken cancellationToken = default
		);
	}
}
