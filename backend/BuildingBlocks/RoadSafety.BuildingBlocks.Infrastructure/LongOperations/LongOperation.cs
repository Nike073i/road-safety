namespace RoadSafety.BuildingBlocks.Infrastructure.LongOperations
{
	public class LongOperation
	{
		public required Guid Id { get; init; }
		public required string OperationJson { get; init; }
		public required DateTimeOffset OcurredOn { get; init; }
		public DateTimeOffset? ScheduledTime { get; init; }
		public string? Error { get; set; }
		public DateTime? ProcessedOn { get; set; }
	}
}
