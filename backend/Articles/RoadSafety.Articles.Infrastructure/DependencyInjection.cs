using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using RoadSafety.Articles.CommandStack;
using RoadSafety.Articles.Contracts;
using RoadSafety.Articles.Infrastructure.EventSourcing;
using RoadSafety.Articles.Infrastructure.LongOperations;
using RoadSafety.Articles.Infrastructure.Persistence;
using RoadSafety.Articles.Infrastructure.Serialization;
using RoadSafety.BuildingBlocks.Application.Clock;
using RoadSafety.BuildingBlocks.Infrastructure.Cache;
using RoadSafety.BuildingBlocks.Infrastructure.Cqrs;
using RoadSafety.BuildingBlocks.Infrastructure.LongOperations;
using RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Worker;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;

namespace RoadSafety.Articles.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			DatabaseSettings databaseSettings,
			LongOperationRunnerOptions options
		) =>
			services
				.AddCaching()
				.AddArticlePersistence(databaseSettings)
				.AddEventSourcing(databaseSettings)
				.AddCqrs(ICommandStackProjectMarker.Assembly, IInfrastructureProjectMarker.Assembly)
				.AddLongOperations<
					ArticleDbContext,
					ArticleLongOperationRunnerJob,
					ArticleLongOperationRunnerJobSetup
				>(options)
				.AddValidatorsFromAssemblies(
					[ICommandStackProjectMarker.Assembly, IContractsProjectMarker.Assembly]
				)
				.AddTransient<IDateTimeProvider, DateTimeProvider>()
				.AddQuartz()
				.AddQuartzHostedService()
				.AddSerializationServices();
	}
}
