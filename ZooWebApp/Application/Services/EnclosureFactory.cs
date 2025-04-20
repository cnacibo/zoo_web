using ZooWebApp.Domain.Models;
using ZooWebApp.Domain.ValueObjects;
using ZooWebApp.Application.Interfaces;
namespace ZooWebApp.Application.Services;
public class EnclosureFactory : IEnclosureFactory{
    public Enclosure Create(string type, string size, int maxAnimals){
        if (!Enum.TryParse<EnclosureType>(type, true, out var enclosureType)){
            throw new ArgumentException("Invalid enclosure type: Predator, Herbivore, Birds");
        }
        return new Enclosure(enclosureType, size, maxAnimals);
    }
}