using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Domain.Articles.Events
{
	public class BlockMovedAfterDomainEvent : DomainEvent
	{
		public required int CurrentPosition { get; init; }
		public required int AfterPosition { get; init; }
	}
}
