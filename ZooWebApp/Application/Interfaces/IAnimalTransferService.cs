using ZooWebApp.Application.DTO;
namespace ZooWebApp.Application.Interfaces;
public interface IAnimalTransferService
{
    Task<IEnumerable<AnimalDto>> GetAllAnimalsAsync();
    Task<AnimalDto> GetAnimalByIdAsync(Guid id);

    Task<Guid> AddAnimalAsync(string species, string name, DateTime birthDate,
        string gender, string favoriteFood, Guid enclosureId);

    Task RemoveAnimalAsync(Guid animalId);

    Task TransferAnimal(Guid animalId, Guid newEnclosureId);

    Task<Guid> AddEnclosureAsync(string type, string size, int maxAnimals);

    Task RemoveEnclosureAsync(Guid id);

    Task<IEnumerable<EnclosureDto>> GetAllEnclosuresAsync();
    Task<EnclosureDto> GetEnclosureByIdAsync(Guid id);
}
