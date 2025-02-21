using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Domain.Articles.Events
{
	public class ArticleVisibilityChangedDomainEvent : DomainEvent
	{
		public required Visibility Visibility { get; init; }
	}
}
