using ZooWebApp.Domain.Models;
namespace ZooWebApp.Domain.Events;
public class AnimalMovedEvent : IDomainEvent
{
    public Animal Animal { get; }

    public AnimalMovedEvent(Animal animal)
    {
        Animal = animal;
    }
}