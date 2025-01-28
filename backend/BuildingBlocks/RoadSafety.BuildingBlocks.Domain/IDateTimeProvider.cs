namespace RoadSafety.BuildingBlocks.Domain
{
	public interface IDateTimeProvider
	{
		DateTimeOffset Now { get; }
	}
}
