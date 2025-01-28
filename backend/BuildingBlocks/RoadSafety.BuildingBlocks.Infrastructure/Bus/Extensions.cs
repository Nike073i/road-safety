using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;

namespace RoadSafety.BuildingBlocks.Infrastructure.Bus
{
	public static class Extensions
	{
		public static IServiceCollection AddRabbitMq<TDbContext>(
			this IServiceCollection services,
			RabbitMqSettings rabbitSettings,
			DatabaseSettings databaseSettings,
			params Assembly[] assemblies
		)
			where TDbContext : BaseDbContext
		{
			services.AddDbContext<TDbContext>(o => o.UseNpgsql(databaseSettings.СonnectionString));
			services.AddMassTransit(bus =>
			{
				bus.AddConsumers(assemblies);
				bus.AddEntityFrameworkOutbox<TDbContext>(c =>
				{
					c.UsePostgres();
					c.UseBusOutbox();
				});
				bus.UsingRabbitMq(
					(context, transport) =>
					{
						transport.Host(
							rabbitSettings.Host,
							rabbitSettings.VHost,
							h =>
							{
								h.Username(rabbitSettings.Username);
								h.Password(rabbitSettings.Password);
							}
						);
						transport.ConfigureEndpoints(context);
					}
				);
			});
			return services;
		}

		public static void UseInbox<TDbContext>(
			this IReceiveEndpointConfigurator configurator,
			IRegistrationContext context
		)
			where TDbContext : BaseDbContext =>
			configurator.UseEntityFrameworkOutbox<TDbContext>(context);
	}
}
