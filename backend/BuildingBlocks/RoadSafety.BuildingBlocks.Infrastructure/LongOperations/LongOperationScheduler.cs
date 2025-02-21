using RoadSafety.BuildingBlocks.Application.Clock;
using RoadSafety.BuildingBlocks.CommandStack.LongOperations;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.BuildingBlocks.Infrastructure.Serialization;

namespace RoadSafety.BuildingBlocks.Infrastructure.LongOperations
{
	public class LongOperationScheduler<TDbContext>(
		TDbContext dbContext,
		ISerializer serializer,
		IDateTimeProvider dateTimeProvider
	) : ILongOperationScheduler
		where TDbContext : BaseDbContext
	{
		public async Task<Guid> AddOperationAsync(
			ILongOperation operation,
			DateTimeOffset? scheduleTime,
			CancellationToken cancellationToken
		)
		{
			string json = serializer.Serialize(operation);
			var longOperation = new LongOperation
			{
				Id = Guid.NewGuid(),
				OperationJson = json,
				OcurredOn = dateTimeProvider.UtcNow,
				ScheduledTime = scheduleTime,
			};
			await dbContext.Set<LongOperation>().AddAsync(longOperation, cancellationToken);
			return longOperation.Id;
		}
	}
}
