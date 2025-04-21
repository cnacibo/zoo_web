namespace ZooWebApp.Domain.Events;
public class AnimalMovedEvent : IDomainEvent
{
    public Guid AnimalId { get; }
    public Guid CurrentEnclosureId { get;}
    public Guid NewEnclosureId { get;}

    public AnimalMovedEvent(Guid animal, Guid currentEnclosureId, Guid newEnclosureId)
    {
        AnimalId = animal;
        CurrentEnclosureId = currentEnclosureId;
        NewEnclosureId = newEnclosureId;
    }
}