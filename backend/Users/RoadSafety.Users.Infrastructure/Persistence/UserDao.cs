using Microsoft.EntityFrameworkCore;
using RoadSafety.Users.CommandStack.Persistence;
using RoadSafety.Users.Domain.Users;
using RoadSafety.Users.Infrastructure.Persistence.Context;

namespace RoadSafety.Users.Infrastructure.Persistence
{
	internal class UserDao(UserDbContext dbContext) : IUserDao
	{
		private readonly UserDbContext _dbContext = dbContext;

		public Task Add(User user, CancellationToken cancellationToken) =>
			_dbContext.AddAsync(user, cancellationToken).AsTask();

		public Task<User?> GetById(Guid id, CancellationToken cancellationToken) =>
			_dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

		public Task UpdateRoles(User user, CancellationToken cancellationToken) =>
			Task.CompletedTask;
	}
}
