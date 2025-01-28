using EntityFramework.Exceptions.Common;
using ErrorOr;
using RoadSafety.Users.CommandStack.Persistence;
using RoadSafety.Users.Infrastructure.Persistence.Context;

namespace RoadSafety.Users.Infrastructure.Persistence
{
	internal class UserUnitOfWork(UserDbContext dbContext, IUserDao userDao) : IUserUnitOfWork
	{
		public IUserDao UserDao => userDao;

		public async Task<ErrorOr<Success>> CommitAsync(CancellationToken cancellationToken)
		{
			try
			{
				await dbContext.SaveChangesAsync(cancellationToken);
				return Result.Success;
			}
			catch (UniqueConstraintException)
			{
				return PersistenceErrors.UserAlreadyExistsError;
			}
		}
	}
}
