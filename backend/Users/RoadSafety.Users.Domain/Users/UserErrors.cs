using System.Globalization;
using System.Runtime.CompilerServices;
using ErrorOr;
using RoadSafety.Users.Domain.Roles;

namespace RoadSafety.Users.Domain.Users
{
	public static class UserErrors
	{
		public static readonly Error UserMustHaveAtLeastOneRoleError = Error.Validation(
			code: CreateCode(),
			description: UserMessages.UserMustHaveAtLeastOneRole
		);

		public static Error UserAlreadyHasRoleError(Role role) =>
			Error.Failure(
				code: CreateCode(),
				description: string.Format(
					CultureInfo.InvariantCulture,
					UserMessages.UserAlreadyHaveRoleWithNameFormat,
					role.Name
				)
			);

		public static Error UserHaveNotRoleError(Role role) =>
			Error.Failure(
				code: CreateCode(),
				description: string.Format(
					CultureInfo.InvariantCulture,
					UserMessages.UserAlreadyHaveRoleWithNameFormat,
					role.Name
				)
			);

		private static string CreateCode([CallerMemberName] string? propertyName = null) =>
			$"User.{propertyName}";
	}
}
