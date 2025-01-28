using System.Runtime.CompilerServices;
using ErrorOr;

namespace RoadSafety.Users.CommandStack.Persistence
{
	public static class PersistenceErrors
	{
		public static readonly Error UserAlreadyExistsError = Error.Conflict(
			code: CreateCode(),
			description: PersestenceMessages.UserAlreadyExists
		);

		private static string CreateCode([CallerMemberName] string? propertyName = null) =>
			$"UserPersistence.{propertyName}";
	}
}
