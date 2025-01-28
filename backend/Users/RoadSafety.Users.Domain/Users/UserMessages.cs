using System.Text;

namespace RoadSafety.Users.Domain.Users
{
	public static class UserMessages
	{
		public const string UserMustHaveAtLeastOneRole = "User must have at least one role";
		public static readonly CompositeFormat UserAlreadyHaveRoleWithNameFormat =
			CompositeFormat.Parse("User already have role with name \"{0}\"");

		public static readonly CompositeFormat UserHaveNotRoleWithNameFormat =
			CompositeFormat.Parse("User haven't role with name \"{0}\"");
	}
}
