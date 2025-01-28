using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoadSafety.BuildingBlocks.Api.Endpoints;
using RoadSafety.Users.CommandStack.Users.Register;
using RoadSafety.Users.Contracts.Common.Users.Register;

namespace RoadSafety.Users.Api.Endpoints.Common.Users.Register
{
	public class RegisterEndpoint
	{
		public static Task<IResult> Handle(
			[FromBody] RegisterRequest input,
			ISender sender,
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
				.ThenAsync(req => sender.Send(req, cancellationToken))
				.MatchResult(value =>
					TypedResults.Created(string.Empty, new RegisterResponse(value))
				);
	}
}
