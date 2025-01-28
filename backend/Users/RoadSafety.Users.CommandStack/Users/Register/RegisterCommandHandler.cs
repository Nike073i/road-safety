using ErrorOr;
using RoadSafety.BuildingBlocks.CommandStack.Cqrs;
using RoadSafety.Users.CommandStack.Authentication;

namespace RoadSafety.Users.CommandStack.Users.Register
{
	public class RegisterCommandHandler(IIdentityProviderClient authenticateClient)
		: ICommandHandler<RegisterCommand, Guid>
	{
		private readonly IIdentityProviderClient _authenticateClient = authenticateClient;

		public Task<ErrorOr<Guid>> Handle(
			RegisterCommand request,
			CancellationToken cancellationToken
		)
		{
			var dto = new RegisterClientDto(
				request.FirstName,
				request.LastName,
				request.UserName,
				request.Email,
				request.Password
			);
			return _authenticateClient.RegisterClientAsync(dto, cancellationToken);
		}
	}
}
