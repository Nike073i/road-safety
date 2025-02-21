using Carter;
using RoadSafety.Articles.Api.Endpoints.Moderator.Articles.Create;
using RoadSafety.Articles.Api.Endpoints.Moderator.Articles.UpdateContent;
using RoadSafety.BuildingBlocks.Api.FeatureManagement;

namespace RoadSafety.Articles.Api.Endpoints.Moderator.Articles
{
	public class ArticleModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("", CreateEndpoint.Handle).RequireFeature<CreateFeatureFilter>();
			app.MapPost("{articleId:guid}/update", UpdateContentEndpoint.Handle)
				.RequireFeature<UpdateContentFeatureFilter>();
		}
	}
}
