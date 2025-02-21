using Marten;
using Marten.Events;
using Marten.Events.Projections;
using RoadSafety.Articles.Domain.Articles.Events;

namespace RoadSafety.Articles.Infrastructure.Articles.Projections
{
	public class ArticleArchivedProjection : EventProjection
	{
		public void Project(IEvent<ArticleArchivedDomainEvent> @event, IDocumentOperations ops)
		{
			var articleId = @event.StreamId;
			ops.QueueSqlCommand(
				"""
					UPDATE articles.articles
					SET 
						is_archived = TRUE
					WHERE 
						id = ?
				""",
				articleId
			);
		}
	}
}
