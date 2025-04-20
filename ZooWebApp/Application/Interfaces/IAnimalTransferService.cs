// using ZooWebApp.Domain.Models;
namespace ZooWebApp.Application.Interfaces;
public interface IAnimalTransferService
{
    Task AddAnimalAsync(string species, string name, DateTime birthDate, 
        string gender, string favoriteFood, Guid enclosureId);
    Task RemoveAnimalAsync(Guid animalId);
    Task TransferAnimal(Guid animalId, Guid newEnclosureId);
    // Task<IEnumerable<Animal>> GetAllAnimalsAsync();
}
