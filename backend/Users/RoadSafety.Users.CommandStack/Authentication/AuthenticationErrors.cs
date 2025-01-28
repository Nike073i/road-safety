using System.Runtime.CompilerServices;
using ErrorOr;

namespace RoadSafety.Users.CommandStack.Authentication
{
	public static class AuthenticationErrors
	{
		public static readonly Error UserAlreadyExistsError = Error.Conflict(
			code: CreateCode(),
			description: AuthenticationMessages.UserAlreadyExists
		);

		public static readonly Error SomethingWentWrongError = Error.Unexpected(
			code: CreateCode(),
			description: AuthenticationMessages.SomethingWentWrong
		);

		private static string CreateCode([CallerMemberName] string? propertyName = null) =>
			$"UserAuthentication.{propertyName}";
	}
}
