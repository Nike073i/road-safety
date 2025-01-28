using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.BuildingBlocks.Application.Logging;
using RoadSafety.BuildingBlocks.Application.Validation;
using RoadSafety.BuildingBlocks.CommandStack.Bus;
using RoadSafety.BuildingBlocks.Infrastructure.Bus;
using RoadSafety.BuildingBlocks.Infrastructure.Cache;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;
using RoadSafety.BuildingBlocks.QueryStack.Cache;
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
			services.AddUserWritePersistence(databaseSettings);
			services.AddUserReadPersistence(databaseSettings);
			services.AddKeycloak(keycloakSettings, adminServiceAccountSetting);
			services.AddMediatR(action =>
			{
				action.AddOpenBehavior(typeof(LoggingBehavior<,>));
				action.AddOpenBehavior(typeof(ValidationBehavior<,>));
				action.AddOpenBehavior(typeof(CachingBehavior<,>));
				action.RegisterServicesFromAssemblies(
					ICommandStackProjectMarker.Assembly,
					IQueryStackProjectMarker.Assembly
				);
			});
			services.AddValidatorsFromAssemblies(
				[ICommandStackProjectMarker.Assembly, IContractsProjectMarker.Assembly]
			);

			services.AddScoped<IEventPublisher, EventPublisher>();

			return services;
		}
	}
}
