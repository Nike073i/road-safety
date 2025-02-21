using Microsoft.FeatureManagement;
using RoadSafety.BuildingBlocks.Api.FeatureManagement;

namespace RoadSafety.Articles.Api.Endpoints.Moderator.Articles.UpdateContent
{
	public class UpdateContentFeatureFilter(IFeatureManager featureManager)
		: FeatureFilterBase(featureManager)
	{
		protected override string FeatureName => "UpdateContent";
	}
}
