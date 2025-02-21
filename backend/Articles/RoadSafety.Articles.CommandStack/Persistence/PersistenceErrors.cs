using System.Runtime.CompilerServices;
using ErrorOr;

namespace RoadSafety.Articles.CommandStack.Persistence
{
	public static class PersistenceErrors
	{
		public static readonly Error ContentAlreadyUpdatedError = Error.Conflict(
			code: CreateCode(),
			description: PersistenceMessages.ContentAlreadyUpdated
		);

		private static string CreateCode([CallerMemberName] string? propertyName = null) =>
			$"ArticlePersistence.{propertyName}";
	}
}
