using RoadSafety.Articles.Domain.Articles.Blocks;
using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Domain.Articles.Events
{
	public class ArticleCreatedDomainEvent : DomainEvent
	{
		public required Guid ArticleId { get; init; }
		public required Guid AuthorId { get; init; }
		public required DateTimeOffset CreatedAt { get; init; }
		public required Visibility Visibility { get; init; }
		public required bool IsEnableCommenting { get; init; }
		public required string Title { get; init; }
		public required string[] Tags { get; init; }
		public required string? Image { get; init; }
		public required string? Subtitle { get; init; }
		public required ArticleBlock[] Blocks { get; init; }
	}
}
