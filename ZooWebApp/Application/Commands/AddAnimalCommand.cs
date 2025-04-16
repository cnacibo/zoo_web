using ZooWebApp.Domain.ValueObjects;
using MediatR;
namespace ZooWebApp.Application.Commands;

public class AddAnimalCommand : IRequest<Unit>
{
    public string Species {get; set;}
    public string Name {get; set;}
    public DateTime BirthDate {get; set;}
    public Gender Gender {get; set;}
    public Food FavoriteFood {get; set;}
    public Guid EnclosureId {get; set;}

    public AddAnimalCommand(string species, string name, DateTime birthDate, Gender gender, Food favoriteFood, Guid enclosureId)
    {
        Species = species;
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        FavoriteFood = favoriteFood;
        EnclosureId = enclosureId;
    }
    
    
}