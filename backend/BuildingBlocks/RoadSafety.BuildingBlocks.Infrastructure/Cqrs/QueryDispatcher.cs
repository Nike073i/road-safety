using MediatR;
using RoadSafety.BuildingBlocks.QueryStack.Cqrs;

namespace RoadSafety.BuildingBlocks.Infrastructure.Cqrs
{
	public class QueryDispatcher(ISender sender) : IQueryDispatcher
	{
		public Task<TResponse> SendQuery<TResponse>(
			IQuery<TResponse> query,
			CancellationToken cancellationToken = default
		) => sender.Send(query, cancellationToken);
	}
}
