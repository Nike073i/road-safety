using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.BuildingBlocks.CommandStack.Bus
{
	public interface IEventPublisher
	{
		Task PublishAsync<TEvent>(
			TEvent integrationEvent,
			CancellationToken cancellationToken = default
		)
			where TEvent : IntegrationEvent;
	}
}
