using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace RoadSafety.BuildingBlocks.Api.FeatureManagement
{
	public static class Extensions
	{
		public static RouteHandlerBuilder RequireFeature<TFeatureFilter>(
			this RouteHandlerBuilder builder
		)
			where TFeatureFilter : FeatureFilterBase => builder.AddEndpointFilter<TFeatureFilter>();
	}
}
