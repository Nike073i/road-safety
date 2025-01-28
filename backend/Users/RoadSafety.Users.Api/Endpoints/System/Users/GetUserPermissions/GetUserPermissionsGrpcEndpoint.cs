using Grpc.Core;
using MediatR;
using RoadSafety.BuildingBlocks.Application.Auth;
using RoadSafety.Users.Contracts.System.Users.GetUserPermissions;
using RoadSafety.Users.QueryStack.Users.GetUserPermissions;
using static RoadSafety.Users.Api.Extensions.GrpcExtensions;

namespace RoadSafety.Users.Api.Endpoints.System.Users.GetUserPermissions
{
	public class GetUserPermissionsGrpcEndpoint(ISender sender, IUserContext userContext)
		: Service.ServiceBase
	{
		private readonly ISender _sender = sender;
		private readonly IUserContext _userContext = userContext;

		public override async Task<Response> Handle(Request request, ServerCallContext context)
		{
			var userId = _userContext.UserInfo!.UserId;
			var result = await _sender.Send(
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
