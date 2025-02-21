using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.BuildingBlocks.Application.Logging;
using RoadSafety.BuildingBlocks.Application.Validation;
using RoadSafety.BuildingBlocks.CommandStack.Cqrs;
using RoadSafety.BuildingBlocks.QueryStack.Cache;
using RoadSafety.BuildingBlocks.QueryStack.Cqrs;

namespace RoadSafety.BuildingBlocks.Infrastructure.Cqrs
{
	public static class Extensions
	{
		public static IServiceCollection AddCqrs(
			this IServiceCollection services,
			params Assembly[] assemblies
		) =>
			services
				.AddMediatR(action =>
				{
					action.AddOpenBehavior(typeof(LoggingBehavior<,>));
					action.AddOpenBehavior(typeof(ValidationBehavior<,>));
					action.AddOpenBehavior(typeof(CachingBehavior<,>));
					action.RegisterServicesFromAssemblies(assemblies);
				})
				.AddScoped<IQueryDispatcher, QueryDispatcher>()
				.AddScoped<ICommandDispatcher, CommandDispatcher>();
	}
}
