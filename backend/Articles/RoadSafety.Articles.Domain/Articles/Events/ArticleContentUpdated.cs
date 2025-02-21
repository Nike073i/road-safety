using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Domain.Articles.Events
{
	public class ArticleContentUpdated : DomainEvent
	{
		public required int PublicVersion { get; init; }
	}
}
