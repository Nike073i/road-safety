using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Domain.Articles.Events
{
	public class ArticleInfoChangedDomainEvent : DomainEvent
	{
		public required string Title { get; init; }
		public required string[] Tags { get; init; }
		public required string? Image { get; init; }
		public required string? Subtitle { get; init; }
	}
}
