using Dapper;
using Quartz;
using RoadSafety.BuildingBlocks.Application.Clock;
using RoadSafety.BuildingBlocks.CommandStack.LongOperations;
using RoadSafety.BuildingBlocks.CommandStack.Persistence;
using RoadSafety.BuildingBlocks.Infrastructure.Serialization;

namespace RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Worker
{
	public abstract class LongOperationRunnerJob(
		IUnitOfWork unitOfWork,
		IDateTimeProvider dateTimeProvider,
		ISerializer serializer,
		ILongOperationDispatcher operationDispatcher
	) : IJob
	{
		protected abstract string Schema { get; }
		private string SelectSql =>
			$"""
				SELECT 
					id {nameof(LongOperation.Id)},
					operation_json {nameof(LongOperation.OperationJson)}
				FROM {Schema}.long_operations
				WHERE 
					processed_on IS NULL AND
					(scheduled_time IS NULL OR scheduled_time <= @CurrentTime)
				ORDER BY ocurred_on
				LIMIT 1
				FOR UPDATE SKIP LOCKED
				""";

		private string UpdateSql =>
			$"""
				UPDATE {Schema}.long_operations
				SET
					processed_on = @ProcessedOn,
					error = @Error
				WHERE id = @Id
				""";

		public async Task Execute(IJobExecutionContext context)
		{
			var connection = unitOfWork.Connection;
			var transaction = await unitOfWork.StartTransactionAsync();

			var longOperation = await connection.QueryFirstOrDefaultAsync<LongOperation>(
				SelectSql,
				new { CurrentTime = dateTimeProvider.UtcNow },
				transaction
			);
			if (longOperation is null)
				return;

			var operation = serializer.Deserialize<ILongOperation>(longOperation.OperationJson)!;
			var result = await operationDispatcher.ExecuteAsync(operation);

			string? error = null;
			if (result.IsError)
				error = result.FirstError.Description;
			await connection.ExecuteAsync(
				UpdateSql,
				new
				{
					longOperation.Id,
					Error = error,
					ProcessedOn = dateTimeProvider.UtcNow,
				}
			);

			await unitOfWork.CommitAsync();
		}
	}
}
