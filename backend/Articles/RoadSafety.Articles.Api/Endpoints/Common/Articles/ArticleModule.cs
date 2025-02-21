using Carter;
using RoadSafety.Articles.Api.Endpoints.Common.Articles.GetArticleDetails;
using RoadSafety.BuildingBlocks.Api.FeatureManagement;

namespace RoadSafety.Articles.Api.Endpoints.Common.Articles
{
	public class ArticleModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app) =>
			app.MapGet("{id:guid}", GetArticleDetailsEndpoint.Handle)
				.RequireFeature<GetArticleDetailsFeatureFilter>();
	}
}
