using Microsoft.FeatureManagement;
using RoadSafety.BuildingBlocks.Api.FeatureManagement;

namespace RoadSafety.Users.Api.Endpoints.Common.Users.Register
{
	public class RegisterFeatureFilter(IFeatureManager featureManager)
		: FeatureFilterBase(featureManager)
	{
		protected override string FeatureName => "Register";
	}
}
