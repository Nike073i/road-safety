using Carter;
using RoadSafety.BuildingBlocks.Api.FeatureManagement;
using RoadSafety.Users.Api.Endpoints.Common.Users.Register;

namespace RoadSafety.Users.Api.Endpoints.Common.Users
{
	public class UserModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app) =>
			app.MapPost("register", RegisterEndpoint.Handle)
				.RequireFeature<RegisterFeatureFilter>();
	}
}
