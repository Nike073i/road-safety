using System.Runtime.CompilerServices;
using ErrorOr;

namespace RoadSafety.Articles.Domain.Articles
{
	public static class ArticleErrors
	{
		public static Error UserHaveNotRightsError =>
			Error.Forbidden(code: CreateCode(), description: ArticleMessages.UserHaveNotRights);

		public static Error UserUnauthorizedError =>
			Error.Unauthorized(code: CreateCode(), description: ArticleMessages.UserHaveNotRights);

		public static Error NoEventsToUpdateContentError =>
			Error.Failure(code: CreateCode(), description: ArticleMessages.NoEventsToUpdateContent);

		public static Error ArticleIsArchivedError =>
			Error.Failure(code: CreateCode(), description: ArticleMessages.ArticleIsArchived);

		public static Error BlockIncorrectPositionError =>
			Error.Failure(code: CreateCode(), description: ArticleMessages.BlockIncorrectPosition);

		public static Error NotFound =>
			Error.NotFound(code: CreateCode(), description: ArticleMessages.ArticleNotFound);

		private static string CreateCode([CallerMemberName] string? propertyName = null) =>
			$"Article.{propertyName}";
	}
}
