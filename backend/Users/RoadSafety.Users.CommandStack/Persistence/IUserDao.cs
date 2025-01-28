using RoadSafety.Users.Domain.Users;

namespace RoadSafety.Users.CommandStack.Persistence
{
	public interface IUserDao
	{
		Task<User?> GetById(Guid id, CancellationToken cancellationToken = default);
		Task Add(User user, CancellationToken cancellationToken = default);
		Task UpdateRoles(User user, CancellationToken cancellationToken = default);
	}
}
