namespace ZooWebApp.Domain.Events;
public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent @event);
}