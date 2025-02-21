using System.Data.Common;
using Dapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RoadSafety.Articles.CommandStack.Persistence;
using RoadSafety.Articles.Domain.Articles;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.BuildingBlocks.Infrastructure.Serialization;
using static Dapper.SqlMapper;

namespace RoadSafety.Articles.Infrastructure.Persistence
{
	public class ArticleDao(ISerializer serializer) : IArticleDao
	{
		private const string _updateSql = """
				UPDATE articles.article_drafts AS d
				SET blocks = @Blocks 
				FROM articles.articles AS a
				WHERE 
					d.article_id = @ArticleId AND 
					a.public_version < @NewVersion;

				UPDATE articles.article_infos AS i
				SET 
					title = @Title, 
					subtitle = @Subtitle, 
					image = @Image, 
					tags = @Tags
				FROM articles.articles AS a
				WHERE 
					i.article_id = @ArticleId AND
					a.public_version < @NewVersion;

				UPDATE articles.articles
				SET 
					last_edited_at = @EditedAt,
					public_version = @NewVersion
				WHERE
					id = @ArticleId AND
					public_version < @NewVersion;
			""";

		public async Task<ErrorOr<Success>> UpdateContent(
			Article article,
			DbConnection connection,
			DbTransaction? transaction,
			DateTimeOffset currentUtc,
			CancellationToken cancellationToken
		)
		{
			string blocks = serializer.Serialize(article.Draft.Blocks);
			int affectedRow = await connection.ExecuteAsync(
				_updateSql,
				new
				{
					ArticleId = article.Id,
					Blocks = new JsonBParameter(blocks),
					article.Info.Title,
					article.Info.Subtitle,
					article.Info.Image,
					article.Info.Tags,
					EditedAt = currentUtc,
					NewVersion = article.PublicVersion,
				},
				transaction
			);
			if (affectedRow == 0)
				return PersistenceErrors.ContentAlreadyUpdatedError;
			return Result.Success;
		}
	}
}
