using Dapper;
using RoadSafety.Articles.QueryStack.Articles.GetArticleDetails;
using RoadSafety.BuildingBlocks.QueryStack.Cqrs;
using RoadSafety.BuildingBlocks.QueryStack.Persistance;

namespace RoadSafety.Articles.Infrastructure.Articles.QueryHandlers
{
	public class GetArticleDetailsQueryHandler(IConnectionFactory connectionFactory)
		: IQueryHandler<GetArticleDetailsQuery, GetArticleDetailsViewModel?>
	{
		private const string _sql = $"""
				SELECT 
					a.id, 
					a.author_id, 
					a.created_at, 
					a.is_archived, 
					a.is_enable_commenting, 
					a.visibility, 
					a.last_edited_at,
					i.title,
					i.subtitle,
					i.image,
					i.tags,
					d.blocks::text AS {nameof(GetArticleDetailsViewModel.BlocksJson)}
				FROM articles.articles AS a
					JOIN articles.article_infos AS i ON a.id = i.article_id
					JOIN articles.article_drafts AS d ON a.id = d.article_id
				WHERE 
					a.id = @Id AND
					a.is_archived = FALSE AND
					a.visibility = 'Visible';
			""";

		public async Task<GetArticleDetailsViewModel?> Handle(
			GetArticleDetailsQuery request,
			CancellationToken cancellationToken
		)
		{
			using var connection = await connectionFactory.OpenConnectionAsync(cancellationToken);
			return await connection.QueryFirstOrDefaultAsync<GetArticleDetailsViewModel>(
				_sql,
				new { request.Id }
			);
		}
	}
}
