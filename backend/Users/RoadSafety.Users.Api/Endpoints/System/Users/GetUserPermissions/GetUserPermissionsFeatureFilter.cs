using Microsoft.FeatureManagement;
using RoadSafety.BuildingBlocks.Api.FeatureManagement;

namespace RoadSafety.Users.Api.Endpoints.System.Users.GetUserPermissions
{
	public class GetUserPermissionsFeatureFilter(IFeatureManager featureManager)
		: FeatureFilterBase(featureManager)
	{
		protected override string FeatureName => "GetUserPermissions";
	}
}
