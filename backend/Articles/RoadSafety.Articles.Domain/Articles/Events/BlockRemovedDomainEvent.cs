using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Domain.Articles.Events
{
	public class BlockRemovedDomainEvent : DomainEvent
	{
		public required int Position { get; init; }
	}
}
