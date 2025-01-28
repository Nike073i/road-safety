using ErrorOr;
using RoadSafety.BuildingBlocks.Domain;
using RoadSafety.Users.Domain.Roles;

namespace RoadSafety.Users.Domain.Users
{
	public class User : Entity
	{
		private HashSet<Role> _roles;
		public IReadOnlyCollection<Role> Roles => _roles.ToList();

#pragma warning disable CS8618
		private User() { }
#pragma warning restore CS8618

		public static ErrorOr<User> Create(Guid userId, ICollection<Role> roles)
		{
			var setOfRoles = new HashSet<Role>(roles);
			if (setOfRoles.Count == 0)
				return UserErrors.UserMustHaveAtLeastOneRoleError;
			var user = new User { Id = userId, _roles = new(roles) };
			return user;
		}

		public ErrorOr<Success> AddRole(Role role) =>
			_roles.Add(role) ? Result.Success : UserErrors.UserAlreadyHasRoleError(role);

		public ErrorOr<Success> RemoveRole(Role role) =>
			_roles.Remove(role) ? Result.Success : UserErrors.UserAlreadyHasRoleError(role);
	}
}
