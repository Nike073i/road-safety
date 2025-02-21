namespace RoadSafety.Articles.Contracts.Moderator.Articles.Create
{
	public record CreateRequest(string Title, string? Subtitle, string? Image, string[] Tags);
}
