using Microsoft.FeatureManagement;
using RoadSafety.BuildingBlocks.Api.FeatureManagement;

namespace RoadSafety.Articles.Api.Endpoints.Common.Articles.GetArticleDetails
{
	public class GetArticleDetailsFeatureFilter(IFeatureManager featureManager)
		: FeatureFilterBase(featureManager)
	{
		protected override string FeatureName => "GetArticleDetails";
	}
}
