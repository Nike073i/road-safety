using Carter;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace RoadSafety.Articles.Api.Endpoints
{
	public static class Extensions
	{
		public static IServiceCollection AddEndpoints(this IServiceCollection services) =>
			services.AddCarter(configurator: c => c.WithModule<RootModule>());

		public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder) =>
			builder.MapGroup("").AddFluentValidationAutoValidation().MapCarter();
	}
}
