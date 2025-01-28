using Carter;
using RoadSafety.BuildingBlocks.Api.Endpoints;
using RoadSafety.Users.Api.Endpoints.Common.Users;

namespace RoadSafety.Users.Api.Endpoints.Common
{
	public class CommonModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app) => app.AddSubmodule<UserModule>("users");
	}
}
