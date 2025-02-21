namespace RoadSafety.Articles.Contracts.Moderator.Articles.UpdateContent
{
	public record UpdateContentRequest(Guid ArticleId, UpdateSettings? UpdateSettings = null);
}
