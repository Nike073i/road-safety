using Marten;
using Marten.Events;
using Marten.Events.Projections;
using RoadSafety.Articles.Domain.Articles.Events;

namespace RoadSafety.Articles.Infrastructure.Articles.Projections
{
	public class ArticleCreatedProjection : EventProjection
	{
		public void Project(IEvent<ArticleCreatedDomainEvent> @event, IDocumentOperations ops)
		{
			var data = @event.Data;
			ops.QueueSqlCommand(
				"""
					INSERT INTO articles.articles (id, author_id, created_at, visibility, is_enable_commenting)
					VALUES (?, ?, ?, ?::articles.visibility, ?)
				""",
				data.ArticleId,
				data.AuthorId,
				data.CreatedAt,
				data.Visibility.Name,
				data.IsEnableCommenting
			);
		}
	}
}
