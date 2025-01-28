using Microsoft.AspNetCore.Builder;
using RoadSafety.BuildingBlocks.Api.Logging;
using Serilog;

namespace RoadSafety.BuildingBlocks.Api.Logging
{
	public static class Extensions
	{
		public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
		{
			builder.Host.UseSerilog(
				(context, configuration) =>
					configuration.ReadFrom.Configuration(context.Configuration)
			);
			return builder;
		}
	}
}
