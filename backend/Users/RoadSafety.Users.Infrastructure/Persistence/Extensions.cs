using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.BuildingBlocks.QueryStack.Persistance;
using RoadSafety.Users.CommandStack.Persistence;
using RoadSafety.Users.Infrastructure.Persistence.Context;
using RoadSafety.Users.Infrastructure.Users.DataSources;
using RoadSafety.Users.QueryStack.Users.GetUserPermissions;

namespace RoadSafety.Users.Infrastructure.Persistence
{
	public static class Extensions
	{
		public static void AddUserWritePersistence(
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
			services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
			services.AddScoped<IUserDao, UserDao>();
		}

		public static void AddUserReadPersistence(
			this IServiceCollection services,
			DatabaseSettings settings
		)
		{
			services.AddTransient<IConnectionFactory>(sp => new NpgsqlConnectionFactory(settings));
			services.AddScoped<IGetUserPermissionsDataSource, GetUserPermissionsDataSource>();
		}
	}
}
