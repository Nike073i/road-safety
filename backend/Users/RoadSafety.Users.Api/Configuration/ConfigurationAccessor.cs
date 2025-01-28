using RoadSafety.BuildingBlocks.Api.Auth;
using RoadSafety.BuildingBlocks.Infrastructure.Bus;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.Users.Infrastructure.Authentication.Keycloak;
using RoadSafety.Users.Infrastructure.Authentication.Settings;

namespace RoadSafety.Users.Api.Configuration
{
	public class ConfigurationAccessor(IConfiguration configuration)
	{
		private readonly IConfiguration _configuration = configuration;

		public DatabaseSettings DatabaseSettings =>
			_configuration
				.GetRequiredSection(ConfigurationKeys.DatabaseSettingsKey)
				.Get<DatabaseSettings>()!;

		public RabbitMqSettings RabbitMqSettings =>
			_configuration
				.GetRequiredSection(ConfigurationKeys.RabbitMqSettingsKey)
				.Get<RabbitMqSettings>()!;

		public KeycloakSettings KeycloakSettings =>
			_configuration
				.GetRequiredSection(ConfigurationKeys.KeycloakSettingsKey)
				.Get<KeycloakSettings>()!;

		public ServiceAccountSettings AdminServiceAccountSetting =>
			_configuration
				.GetRequiredSection(ConfigurationKeys.AdminServiceAccountSettingsKey)
				.Get<ServiceAccountSettings>()!;

		public JwtBearerSettings JwtBearerSettings =>
			_configuration
				.GetRequiredSection(ConfigurationKeys.JwtAuthenticationSettingsKey)
				.Get<JwtBearerSettings>()!;
	}
}
