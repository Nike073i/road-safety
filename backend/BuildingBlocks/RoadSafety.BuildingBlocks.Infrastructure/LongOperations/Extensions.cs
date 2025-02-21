using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.BuildingBlocks.CommandStack.LongOperations;
using RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Worker;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;

namespace RoadSafety.BuildingBlocks.Infrastructure.LongOperations
{
	public static class Extensions
	{
		public static IServiceCollection AddLongOperations<TDbContext, TJob, TJobSetup>(
			this IServiceCollection services,
			LongOperationRunnerOptions options
		)
			where TDbContext : BaseDbContext
			where TJob : LongOperationRunnerJob
			where TJobSetup : LongOperationRunnerJobSetup<TJob> =>
			services
				.AddScoped<ILongOperationDispatcher, LongOperationDispatcher>()
				.AddScoped<ILongOperationScheduler, LongOperationScheduler<TDbContext>>()
				.AddSingleton(options)
				.ConfigureOptions<TJobSetup>();

		public static void AddLongOperations(this ModelBuilder modelBuilder, string schema)
		{
			modelBuilder.Entity<LongOperation>(builder =>
				builder.ToTable("long_operations", schema)
			);
		}
	}
}
