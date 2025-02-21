using System.Data.Common;
using EntityFramework.Exceptions.Common;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.BuildingBlocks.QueryStack.Persistance;
using RoadSafety.Users.CommandStack.Persistence;
using RoadSafety.Users.Infrastructure.Persistence.Context;

namespace RoadSafety.Users.Infrastructure.Persistence
{
	internal class UserUnitOfWork : UnitOfWork
	{
		private readonly UserDbContext _dbContext;

		private UserUnitOfWork(UserDbContext dbContext, DbConnection connection)
			: base(connection)
		{
			_dbContext = dbContext;
		}

		protected override async Task<ErrorOr<Success>> SaveActions(
			CancellationToken cancellationToken
		)
		{
			_dbContext.Database.SetDbConnection(Connection);
			_dbContext.Database.UseTransaction(Transaction);
			try
			{
				await _dbContext.SaveChangesAsync(cancellationToken);
				return Result.Success;
			}
			catch (UniqueConstraintException)
			{
				return PersistenceErrors.UserAlreadyExistsError;
			}
		}

		public static async Task<UserUnitOfWork> CreateAsync(
			UserDbContext dbContext,
			IConnectionFactory connectionFactory
		)
		{
			var connection = await connectionFactory.OpenConnectionAsync();
			var unitOfWork = new UserUnitOfWork(dbContext, connection);
			return unitOfWork;
		}
	}
}
