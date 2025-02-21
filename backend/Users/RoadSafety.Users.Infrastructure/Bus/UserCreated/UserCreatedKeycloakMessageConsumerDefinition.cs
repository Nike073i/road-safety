using MassTransit;
using RabbitMQ.Client;

namespace RoadSafety.Users.Infrastructure.Bus.UserCreated
{
	public class UserCreatedKeycloakMessageConsumerDefinition
		: ConsumerDefinition<UserCreatedKeycloakMessageConsumer>
	{
		public UserCreatedKeycloakMessageConsumerDefinition()
		{
			EndpointName = "keycloak-user-created";
		}

		protected override void ConfigureConsumer(
			IReceiveEndpointConfigurator endpointConfigurator,
			IConsumerConfigurator<UserCreatedKeycloakMessageConsumer> consumerConfigurator,
			IRegistrationContext context
		)
		{
			endpointConfigurator.ConfigureConsumeTopology = false;
			endpointConfigurator.UseRawJsonDeserializer(RawSerializerOptions.All);
			if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbit)
			{
				rabbit.Bind(
					"amq.topic",
					b =>
					{
						b.ExchangeType = ExchangeType.Topic;
						b.RoutingKey = "KK.EVENT.ADMIN.road-safety.SUCCESS.USER.CREATE";
					}
				);
			}
		}
	}
}
