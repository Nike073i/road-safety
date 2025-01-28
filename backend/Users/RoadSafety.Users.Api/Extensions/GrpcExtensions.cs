using Grpc.Core;
using RoadSafety.BuildingBlocks.Api.FeatureManagement;

namespace RoadSafety.Users.Api.Extensions
{
	public static class GrpcExtensions
	{
		public static void GrpcNotFound(string description = "") =>
			ThrowRpcException(description, StatusCode.NotFound);

		public static void GrpcNotAuthorized(string description = "") =>
			ThrowRpcException(description, StatusCode.PermissionDenied);

		private static void ThrowRpcException(string description, StatusCode statusCode) =>
			throw new RpcException(new Status(statusCode, description));

		public static GrpcServiceEndpointConventionBuilder RequireFeature<TFeatureFilter>(
			this GrpcServiceEndpointConventionBuilder builder
		)
			where TFeatureFilter : FeatureFilterBase =>
			builder.AddEndpointFilter<GrpcServiceEndpointConventionBuilder, TFeatureFilter>();
	}
}
