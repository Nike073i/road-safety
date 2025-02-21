using Marten;
using Marten.Events;
using Marten.Events.Projections;
using RoadSafety.Articles.Domain.Articles.Events;

namespace RoadSafety.Articles.Infrastructure.Articles.Projections
{
	public class ArticleVisibilityChangedProjection : EventProjection
	{
		public void Project(
			IEvent<ArticleVisibilityChangedDomainEvent> @event,
			IDocumentOperations ops
		)
		{
			var data = @event.Data;
			var articleId = @event.StreamId;
			ops.QueueSqlCommand(
				"""
					UPDATE articles.articles
					SET 
						visibility = ?::articles.visibility
					WHERE 
						id = ?
				""",
				data.Visibility.Name,
				articleId
			);
		}
	}
}
