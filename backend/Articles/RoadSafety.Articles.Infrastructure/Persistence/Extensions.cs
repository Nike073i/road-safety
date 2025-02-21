using Dapper;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.Articles.CommandStack.Persistence;
using RoadSafety.Articles.Infrastructure.EventSourcing;
using RoadSafety.BuildingBlocks.CommandStack.Persistence;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.BuildingBlocks.QueryStack.Persistance;

namespace RoadSafety.Articles.Infrastructure.Persistence
{
	public static class Extensions
	{
		public static IServiceCollection AddArticlePersistence(
			this IServiceCollection services,
			DatabaseSettings settings
		)
		{
			DefaultTypeMap.MatchNamesWithUnderscores = true;
			services.AddTransient<IConnectionFactory, NpgsqlConnectionFactory>();
			services.AddDbContext<ArticleDbContext>(
				(sp, builder) =>
					builder.UseNpgsql(settings.СonnectionString).UseExceptionProcessor()
			);
			services.AddScoped<IArticleDao, ArticleDao>();
			services.AddScoped<IUnitOfWork>(sp =>
				ArticleUnitOfWork
					.CreateAsync(
						sp.GetRequiredService<ArticleDbContext>(),
						sp.GetRequiredService<IConnectionFactory>(),
						sp.GetRequiredService<IEventContext>()
					)
					.GetAwaiter()
					.GetResult()
			);
			services.AddNpgsqlDataSource(settings.СonnectionString);
			return services;
		}
	}
}
