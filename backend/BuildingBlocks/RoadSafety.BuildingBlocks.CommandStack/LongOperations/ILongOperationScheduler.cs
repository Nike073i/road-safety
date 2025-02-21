namespace RoadSafety.BuildingBlocks.CommandStack.LongOperations
{
	public interface ILongOperationScheduler
	{
		Task<Guid> AddOperationAsync(
			ILongOperation operation,
			DateTimeOffset? scheduleTime = null,
			CancellationToken cancellationToken = default
		);
	}
}
