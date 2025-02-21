using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RoadSafety.BuildingBlocks.Api.Endpoints;
using RoadSafety.BuildingBlocks.CommandStack.Cqrs;
using RoadSafety.Users.CommandStack.Users.Register;
using RoadSafety.Users.Contracts.Common.Users.Register;

namespace RoadSafety.Users.Api.Endpoints.Common.Users.Register
{
	public class RegisterEndpoint
	{
		public static Task<IResult> Handle(
			[FromBody] RegisterRequest input,
			ICommandDispatcher dispatcher,
			CancellationToken cancellationToken
		) =>
			input
				.ToErrorOr()
				.Then(input => new RegisterCommand(
					input.FirstName,
					input.LastName,
					input.UserName,
					input.Email,
					input.Password
				))
				.ThenAsync(req => dispatcher.SendCommand(req, cancellationToken))
				.MatchResult(value =>
					TypedResults.Created(string.Empty, new RegisterResponse(value))
				);
	}
}
