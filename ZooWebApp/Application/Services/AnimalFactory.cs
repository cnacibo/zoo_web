using ZooWebApp.Domain.Models;
using ZooWebApp.Domain.ValueObjects;
using ZooWebApp.Application.Interfaces;
namespace ZooWebApp.Application.Services;
public class AnimalFactory : IAnimalFactory
{
    public Animal Create(string species, string name, DateTime birthDate, 
        string gender, string favoriteFood, Guid enclosureId)
    {
        if (string.IsNullOrWhiteSpace(name)) { 
            throw new ArgumentException("Name cannot be empty");
        }

        if (!Enum.TryParse<Gender>(gender, out var parsedGender)) {
            throw new ArgumentException("Invalid gender: Male, Female");
        }
        if (birthDate > DateTime.UtcNow) {
            throw new ArgumentException("Birth date cannot be in the future");
        }

        if (!Enum.TryParse<Food>(favoriteFood, out var parsedFood))
            throw new ArgumentException("Invalid favorite food: Meat, Fish, Grass, Vegetables, Fruit");


        return new Animal(
            species: species,
            name: name,
            birthDate: birthDate,
            gender: parsedGender,
            favoriteFood: parsedFood,
            enclosureId: enclosureId
        );
    }
}
