using Marten;
using Marten.Events;
using Marten.Events.Projections;
using RoadSafety.Articles.Domain.Articles.Events;

namespace RoadSafety.Articles.Infrastructure.Articles.Projections
{
	public class ArticleCommentingSettingsChangedProjection : EventProjection
	{
		public void Project(
			IEvent<ArticleCommentingSettingsChangedDomainEvent> @event,
			IDocumentOperations ops
		)
		{
			var data = @event.Data;
			var articleId = @event.StreamId;
			ops.QueueSqlCommand(
				"""
					UPDATE articles.articles
					SET 
						is_enable_commenting = ?
					WHERE 
						id = ?
				""",
				data.IsEnableCommenting,
				articleId
			);
		}
	}
}
