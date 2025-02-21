using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Domain.Articles.Events
{
	public class ArticleCommentingSettingsChangedDomainEvent : DomainEvent
	{
		public bool IsEnableCommenting { get; init; }
	}
}
