using ZooWebApp.Domain.Events;

namespace ZooWebApp.Application.Events;
public interface IDomainEventService
{
    void Raise(IDomainEvent domainEvent);
    event Action<IDomainEvent> OnDomainEvent;
}
