using RoadSafety.BuildingBlocks.CommandStack.Cqrs;

namespace RoadSafety.Articles.CommandStack.Articles.UpdateContent
{
	public record UpdateContentCommand(
		Guid ArticleId,
		DateTimeOffset? ScheduledTime = null,
		int? Version = null
	) : ICommand<Guid>;
}
