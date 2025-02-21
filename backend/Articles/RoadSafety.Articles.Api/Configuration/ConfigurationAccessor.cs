using RoadSafety.BuildingBlocks.Api.Auth;
using RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Worker;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;

namespace RoadSafety.Articles.Api.Configuration
{
	public class ConfigurationAccessor(IConfiguration configuration)
	{
		private readonly IConfiguration _configuration = configuration;

		public DatabaseSettings DatabaseSettings =>
			_configuration
				.GetRequiredSection(ConfigurationKeys.DatabaseSettingsKey)
				.Get<DatabaseSettings>()!;

		public JwtBearerSettings JwtBearerSettings =>
			_configuration
				.GetRequiredSection(ConfigurationKeys.JwtAuthenticationSettingsKey)
				.Get<JwtBearerSettings>()!;

		public LongOperationRunnerOptions LongOperationRunnerOptions =>
			_configuration
				.GetRequiredSection(ConfigurationKeys.LongOperationRunnerOptionsSettingsKey)
				.Get<LongOperationRunnerOptions>()!;
	}
}
