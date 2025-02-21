using System.Text.Json;

namespace RoadSafety.Articles.Contracts.Common.Articles.GetArticleDetails
{
	public record GetArticleDetailsResponse(
		Guid Id,
		Guid AuthorId,
		DateTimeOffset CreatedAt,
		bool IsArchived,
		bool IsEnableCommenting,
		string Visibility,
		DateTimeOffset? LastEditedAt,
		string Title,
		string? Subtitle,
		string? Image,
		string[] Tags,
		JsonDocument Blocks
	);
}
