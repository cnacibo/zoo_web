using ZooWebApp.Domain.Events;
namespace ZooWebApp.Application.Interfaces;
public interface IEventRepository
{
    Task Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
}