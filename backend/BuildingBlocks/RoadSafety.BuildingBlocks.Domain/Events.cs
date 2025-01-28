using MediatR;

namespace RoadSafety.BuildingBlocks.Domain
{
	public abstract class SystemEvent : INotification
	{
		public Guid Id { get; init; } = Guid.NewGuid();
		public required DateTimeOffset OccuredOn { get; init; }
	}

	public abstract class DomainEvent : SystemEvent { }

	public abstract class IntegrationEvent : SystemEvent;

	public interface IDomainEventSubscriber<TEvent> : INotificationHandler<TEvent>
		where TEvent : DomainEvent;

	public interface IIntegrationEventSubscriber<TEvent> : INotificationHandler<TEvent>
		where TEvent : IntegrationEvent;
}
