using ZooWebApp.Domain.Events;
using ZooWebApp.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace ZooWebApp.Infrastructure.Repositories;

public class InMemoryEventRepository : IEventRepository
{
    private readonly IServiceProvider _serviceProvider;
    private readonly List<object> _publishedEvents = new();

    public InMemoryEventRepository(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
    {
        using var scope = _serviceProvider.CreateScope();
        _publishedEvents.Add(@event);
        Console.WriteLine($"Event published: {@event.GetType().Name}");
        
        // Получаем все обработчики из DI
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
        foreach (var handler in handlers)
        {
            try
            {
                await handler.Handle(@event);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling event {typeof(TEvent).Name}");          
            }
        }
    }
}