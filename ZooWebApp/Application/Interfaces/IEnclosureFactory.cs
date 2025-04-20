using ZooWebApp.Domain.Models;
namespace ZooWebApp.Application.Interfaces;
public interface IEnclosureFactory{
    Enclosure Create(string type, string size, int maxAnimals);
}