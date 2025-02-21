using Grpc.Core;
using RoadSafety.BuildingBlocks.Application.Auth;
using RoadSafety.BuildingBlocks.QueryStack.Cqrs;
using RoadSafety.Users.Contracts.System.Users.GetUserPermissions;
using RoadSafety.Users.QueryStack.Users.GetUserPermissions;
using static RoadSafety.Users.Api.Extensions.GrpcExtensions;

namespace RoadSafety.Users.Api.Endpoints.System.Users.GetUserPermissions
{
	public class GetUserPermissionsGrpcEndpoint(
		IQueryDispatcher dispatcher,
		IUserContext userContext
	) : Service.ServiceBase
	{
		public override async Task<Response> Handle(Request request, ServerCallContext context)
		{
			var userId = userContext.UserInfo!.UserId;
			var result = await dispatcher.SendQuery(
				new GetUserPermissionsQuery(userId),
				context.CancellationToken
			);
			if (result is null)
				GrpcNotFound();
			var response = new Response { UserId = userId.ToString() };
			response.Permissions.AddRange(
				result!.Permissions.Select(p => new Permission { Code = p.Code, Name = p.Name })
			);
			return response;
		}
	}
}
