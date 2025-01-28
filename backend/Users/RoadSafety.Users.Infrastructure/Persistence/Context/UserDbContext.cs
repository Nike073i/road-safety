using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.Users.Domain.Roles;
using RoadSafety.Users.Domain.Users;

namespace RoadSafety.Users.Infrastructure.Persistence.Context
{
	internal class UserDbContext(DbContextOptions<UserDbContext> options) : BaseDbContext(options)
	{
		public DbSet<User> Users => Set<User>();
		public DbSet<Role> Roles => Set<Role>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema("users");
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
