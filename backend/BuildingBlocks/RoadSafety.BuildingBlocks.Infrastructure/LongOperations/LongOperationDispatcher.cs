using ErrorOr;
using MediatR;
using RoadSafety.BuildingBlocks.CommandStack.LongOperations;

namespace RoadSafety.BuildingBlocks.Infrastructure.LongOperations
{
	public class LongOperationDispatcher(ISender sender) : ILongOperationDispatcher
	{
		public Task<ErrorOr<Success>> ExecuteAsync(
			ILongOperation longOperation,
			CancellationToken cancellationToken
		) => sender.Send(longOperation, cancellationToken);
	}
}
