using RoadSafety.Articles.Domain.Articles.Blocks;
using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Domain.Articles.Events
{
	public class BlockInsertDomainEvent : DomainEvent
	{
		public required int Position { get; init; }
		public required ArticleBlock Block { get; init; }
	}
}
