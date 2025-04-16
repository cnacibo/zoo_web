using ZooWebApp.Domain.ValueObjects;
namespace ZooWebApp.Domain.Models;
public class Animal
{
    public Guid Id { get; private set; }
    public string Species { get; private set; }
    public string Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public Food FavoriteFood { get; private set; }
    public bool IsHealthy { get; private set; }
    public Guid EnclosureId { get; private set; }

    public Animal(string species, string name, DateTime birthDate, 
        Gender gender, Food favoriteFood, Guid enclosureId)
    {
        Id = Guid.NewGuid();
        Species = species;
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        FavoriteFood = favoriteFood;
        IsHealthy = true;
        EnclosureId = enclosureId;
    }

    public void Feed()
    {
        // Логика кормления
        IsHealthy = true;
    }

    public void Treat()
    {
        IsHealthy = true;
    }

    public void MoveToEnclosure(Guid enclosureId)
    {
        EnclosureId = enclosureId;
        // DomainEvents.Raise(new AnimalMovedEvent(this));
    }
}