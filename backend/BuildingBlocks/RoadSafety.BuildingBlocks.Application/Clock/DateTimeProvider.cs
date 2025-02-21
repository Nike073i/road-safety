namespace RoadSafety.BuildingBlocks.Application.Clock
{
	public class DateTimeProvider : IDateTimeProvider
	{
		public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
	}
}
