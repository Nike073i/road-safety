using Microsoft.Extensions.DependencyInjection;
using RoadSafety.BuildingBlocks.Application.Cache;

namespace RoadSafety.BuildingBlocks.Infrastructure.Cache
{
	public static class Extensions
	{
		public static IServiceCollection AddCaching(this IServiceCollection services)
		{
			services.AddTransient<ICacheService, CacheService>();
			services.AddFusionCache();

			return services;
		}
	}
}
