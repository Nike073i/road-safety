using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.BuildingBlocks.CommandStack.Cqrs
{
	public interface IEventDispatcher
	{
		Task Publish<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default)
			where TEvent : DomainEvent;
	}
}
