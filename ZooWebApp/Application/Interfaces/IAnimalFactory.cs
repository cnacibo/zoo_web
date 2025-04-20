using ZooWebApp.Domain.Models;
namespace ZooWebApp.Application.Interfaces;
public interface IAnimalFactory
{
    Animal Create(string species, string name, DateTime birthDate, string gender, string favoriteFood, Guid enclosureId);
}
