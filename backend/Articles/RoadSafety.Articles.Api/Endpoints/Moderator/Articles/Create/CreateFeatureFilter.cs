using Microsoft.FeatureManagement;
using RoadSafety.BuildingBlocks.Api.FeatureManagement;

namespace RoadSafety.Articles.Api.Endpoints.Moderator.Articles.Create
{
	public class CreateFeatureFilter(IFeatureManager featureManager)
		: FeatureFilterBase(featureManager)
	{
		protected override string FeatureName => "CreateArticle";
	}
}
