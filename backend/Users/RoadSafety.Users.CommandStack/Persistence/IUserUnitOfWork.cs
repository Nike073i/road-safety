using RoadSafety.BuildingBlocks.CommandStack.Persistence;

namespace RoadSafety.Users.CommandStack.Persistence
{
	public interface IUserUnitOfWork : IUnitOfWork
	{
		IUserDao UserDao { get; }
	}
}
