using Microsoft.Extensions.DependencyInjection;
namespace ZooWebApp.Domain.Events;
public static class DomainEvents
{
    private static IServiceProvider _serviceProvider;

    public static void Configure(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static async Task Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent
    {
        if (_serviceProvider == null) return;
        
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
        foreach (var handler in handlers)
        {
            await handler.Handle(@event);
        }
    }
}