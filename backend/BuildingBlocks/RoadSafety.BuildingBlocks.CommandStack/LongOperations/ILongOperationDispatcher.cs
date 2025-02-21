using ErrorOr;

namespace RoadSafety.BuildingBlocks.CommandStack.LongOperations
{
	public interface ILongOperationDispatcher
	{
		Task<ErrorOr<Success>> ExecuteAsync(
			ILongOperation longOperation,
			CancellationToken cancellationToken = default
		);
	}
}
