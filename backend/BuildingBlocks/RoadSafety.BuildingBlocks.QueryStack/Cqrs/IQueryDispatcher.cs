namespace RoadSafety.BuildingBlocks.QueryStack.Cqrs
{
	public interface IQueryDispatcher
	{
		Task<TResponse> SendQuery<TQuery, TResponse>(
			TQuery query,
			CancellationToken cancellationToken = default
		)
			where TQuery : IQuery<TResponse>;
	}
}
