using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.BuildingBlocks.Application.Clock;
using RoadSafety.BuildingBlocks.CommandStack.Bus;
using RoadSafety.BuildingBlocks.Infrastructure.Bus;
using RoadSafety.BuildingBlocks.Infrastructure.Cache;
using RoadSafety.BuildingBlocks.Infrastructure.Cqrs;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.Users.CommandStack;
using RoadSafety.Users.Contracts;
using RoadSafety.Users.Infrastructure.Authentication;
using RoadSafety.Users.Infrastructure.Authentication.Keycloak;
using RoadSafety.Users.Infrastructure.Authentication.Settings;
using RoadSafety.Users.Infrastructure.Persistence;
using RoadSafety.Users.Infrastructure.Persistence.Context;
using RoadSafety.Users.QueryStack;

namespace RoadSafety.Users.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			RabbitMqSettings rabbitMqSettings,
			DatabaseSettings databaseSettings,
			KeycloakSettings keycloakSettings,
			ServiceAccountSettings adminServiceAccountSetting
		)
		{
			services.AddRabbitMq<UserDbContext>(
				rabbitMqSettings,
				databaseSettings,
				IInfrastructureProjectMarker.Assembly
			);
			services.AddCaching();
			services.AddUserPersistence(databaseSettings);
			services.AddKeycloak(keycloakSettings, adminServiceAccountSetting);
			services.AddCqrs(
				ICommandStackProjectMarker.Assembly,
				IQueryStackProjectMarker.Assembly
			);
			services.AddValidatorsFromAssemblies(
				[ICommandStackProjectMarker.Assembly, IContractsProjectMarker.Assembly]
			);
			services.AddTransient<IDateTimeProvider, DateTimeProvider>();
			services.AddScoped<IEventPublisher, EventPublisher>();

			return services;
		}
	}
}
