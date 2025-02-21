namespace RoadSafety.Articles.Contracts.Moderator.Articles.UpdateContent
{
	public record UpdateSettings(int? Version = null, DateTimeOffset? ScheduledTime = null);
}
