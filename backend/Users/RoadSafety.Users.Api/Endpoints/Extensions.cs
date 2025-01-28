using Carter;
using RoadSafety.Users.Api.Endpoints.System.Users.GetUserPermissions;
using RoadSafety.Users.Api.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace RoadSafety.Users.Api.Endpoints
{
	public static class Extensions
	{
		public static IServiceCollection AddEndpoints(this IServiceCollection services)
		{
			services.AddCarter(configurator: c => c.WithModule<RootModule>());
			services.AddGrpc();
			return services;
		}

		public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder) =>
			builder.MapGroup("").AddFluentValidationAutoValidation().MapCarter();

		public static IEndpointRouteBuilder MapGrpc(this IEndpointRouteBuilder builder)
		{
			builder
				.MapGrpcService<GetUserPermissionsGrpcEndpoint>()
				.RequireAuthorization()
				.RequireFeature<GetUserPermissionsFeatureFilter>();
			return builder;
		}
	}
}
