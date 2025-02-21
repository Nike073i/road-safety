using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.BuildingBlocks.CommandStack.Persistence;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.BuildingBlocks.QueryStack.Persistance;
using RoadSafety.Users.CommandStack.Persistence;
using RoadSafety.Users.Infrastructure.Persistence.Context;

namespace RoadSafety.Users.Infrastructure.Persistence
{
	public static class Extensions
	{
		public static void AddUserPersistence(
			this IServiceCollection services,
			DatabaseSettings settings
		)
		{
			services.AddTransient<IgnoreEntitiesInterceptor>();
			services.AddDbContext<UserDbContext>(
				(sp, builder) =>
					builder
						.UseNpgsql(settings.СonnectionString)
						.UseExceptionProcessor()
						.AddInterceptors(sp.GetRequiredService<IgnoreEntitiesInterceptor>())
			);
			services.AddScoped<IUnitOfWork>(sp =>
				UserUnitOfWork
					.CreateAsync(
						sp.GetRequiredService<UserDbContext>(),
						sp.GetRequiredService<IConnectionFactory>()
					)
					.GetAwaiter()
					.GetResult()
			);
			services.AddNpgsqlDataSource(settings.СonnectionString);
			services.AddScoped<IUserDao, UserDao>();
			services.AddTransient<IConnectionFactory, NpgsqlConnectionFactory>();
		}
	}
}
