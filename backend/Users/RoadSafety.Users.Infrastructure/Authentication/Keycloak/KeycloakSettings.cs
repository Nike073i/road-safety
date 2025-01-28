namespace RoadSafety.Users.Infrastructure.Authentication.Keycloak
{
	public class KeycloakSettings
	{
		public required string AdminAddress { get; set; }
		public required string OpenIdAddress { get; set; }

		public Uri GetAdminUri() => new(AdminAddress);

		public Uri GetOpenIdUri() => new(OpenIdAddress);
	}
}
