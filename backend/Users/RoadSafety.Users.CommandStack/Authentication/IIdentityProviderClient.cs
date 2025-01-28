using ErrorOr;

namespace RoadSafety.Users.CommandStack.Authentication
{
	public interface IIdentityProviderClient
	{
		Task<ErrorOr<Guid>> RegisterClientAsync(
			RegisterClientDto registerClientDto,
			CancellationToken cancellationToken = default
		);
	}
}
