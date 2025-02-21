namespace RoadSafety.Articles.QueryStack.Articles.GetArticleDetails
{
	public class GetArticleDetailsViewModel
	{
		public required Guid Id { get; init; }
		public required Guid AuthorId { get; init; }
		public required DateTimeOffset CreatedAt { get; init; }
		public required bool IsArchived { get; init; }
		public required bool IsEnableCommenting { get; init; }
		public required string Visibility { get; init; }
		public required DateTimeOffset? LastEditedAt { get; init; }
		public required string Title { get; init; }
		public required string? Subtitle { get; init; }
		public required string? Image { get; init; }
		public required string[] Tags { get; init; }
		public required string BlocksJson { get; init; }
	}
}
