using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement;

namespace RoadSafety.BuildingBlocks.Api.FeatureManagement
{
	public abstract class FeatureFilterBase(IFeatureManager featureManager) : IEndpointFilter
	{
		private readonly IFeatureManager _featureManager = featureManager;

		protected abstract string FeatureName { get; }

		public async ValueTask<object?> InvokeAsync(
			EndpointFilterInvocationContext context,
			EndpointFilterDelegate next
		)
		{
			bool featureEnabled = await _featureManager.IsEnabledAsync(FeatureName);
			return featureEnabled ? await next(context) : TypedResults.NotFound();
		}
	}
}
