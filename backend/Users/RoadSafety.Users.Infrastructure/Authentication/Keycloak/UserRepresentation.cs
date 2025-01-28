using System.Text.Json.Serialization;
using RoadSafety.Users.CommandStack.Authentication;

namespace RoadSafety.Users.Infrastructure.Authentication.Keycloak
{
	internal class UserRepresentation
	{
		[JsonPropertyName("firstName")]
		public string FirstName { get; private set; }

		[JsonPropertyName("lastName")]
		public string LastName { get; private set; }

		[JsonPropertyName("username")]
		public string Username { get; private set; }

		[JsonPropertyName("email")]
		public string Email { get; private set; }

		[JsonPropertyName("enabled")]
		public bool Enabled { get; private set; }

		[JsonPropertyName("requiredActions")]
		public string[] RequiredActions { get; private set; }

		[JsonPropertyName("credentials")]
		public CredentialRepresentation[] Credentials { get; private set; }

#pragma warning disable CS8618
		private UserRepresentation() { }
#pragma warning restore CS8618

		public static UserRepresentation FromDto(RegisterClientDto dto) =>
			new()
			{
				Email = dto.Email,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Username = dto.UserName,
				RequiredActions = ["VERIFY_EMAIL"],
				Enabled = true,
				Credentials =
				[
					new CredentialRepresentation
					{
						Temporary = false,
						Type = "password",
						Value = dto.Password,
					},
				],
			};
	}
}
