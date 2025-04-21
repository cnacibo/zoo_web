namespace ZooWebApp.Domain.Events;
public static class DomainEvents
{
    public static void Raise<T>(T domainEvent) where T : IDomainEvent
    {
        // Здесь будет логика обработки событий
    }
}