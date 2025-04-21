using Microsoft.VisualBasic;
using ZooWebApp.Application.DTO;
using ZooWebApp.Application.Interfaces;
namespace ZooWebApp.Application.Services;
public class AnimalTransferService : IAnimalTransferService
{
    private readonly IAnimalRepository  _animalRepository; 
    private readonly IEnclosureRepository _enclosureRepository;
    private readonly IAnimalFactory _animalFactory;
    private readonly IEnclosureFactory _enclosureFactory;

    public AnimalTransferService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository, 
    IAnimalFactory animalFactory, IEnclosureFactory enclosureFactory)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
        _animalFactory = animalFactory;
        _enclosureFactory = enclosureFactory;
    }

    public async Task<IEnumerable<AnimalDto>> GetAllAnimalsAsync()
    {
        var animals = await _animalRepository.GetAllAsync();
        return animals.Select(a => new AnimalDto
        {
            Id = a.Id,
            Species = a.Species.ToString(),
            Name = a.Name,
            BirthDate = a.BirthDate,
            Gender = a.Gender.ToString(),
            FavoriteFood = a.FavoriteFood.ToString(),
            IsHealthy = a.IsHealthy,
            EnclosureId = a.EnclosureId,
        });

    }

    public async Task<AnimalDto> GetAnimalByIdAsync(Guid id)
    {
        var animal = await _animalRepository.GetByIdAsync(id);
        if (animal == null) return null;
        
        return new AnimalDto
        {
            Id = animal.Id,
            Species = animal.Species.ToString(),
            Name = animal.Name,
            BirthDate = animal.BirthDate,
            Gender = animal.Gender.ToString(),
            FavoriteFood = animal.FavoriteFood.ToString(),
            IsHealthy = animal.IsHealthy,
            EnclosureId = animal.EnclosureId
        };
    }

    public async Task<Guid> AddAnimalAsync(string species, string name, DateTime birthDate, 
        string gender, string favoriteFood, Guid enclosureId)
    {

        var enclosure = await _enclosureRepository.GetByIdAsync(enclosureId);
        if (enclosure == null)
            throw new InvalidOperationException("Enclosure not found");

        var animal = _animalFactory.Create(species, name, birthDate, gender, favoriteFood, enclosureId);

        enclosure.AddAnimal(animal.Id);
        await _animalRepository.AddAsync(animal);
        await _enclosureRepository.UpdateAsync(enclosure);
        return animal.Id;
    }

    public async Task RemoveAnimalAsync(Guid animalId)
    {
        var animal = await _animalRepository.GetByIdAsync(animalId);
        if (animal == null)
            throw new InvalidOperationException("Animal not found");

        var enclosure = await _enclosureRepository.GetByIdAsync(animal.EnclosureId);
        if (enclosure == null)
            throw new InvalidOperationException("Enclosure not found");

        enclosure.RemoveAnimal(animal.Id);
        await _animalRepository.DeleteAsync(animalId);
        await _enclosureRepository.UpdateAsync(enclosure);
    }

    public async Task TransferAnimal(Guid animalId, Guid newEnclosureId)
    {
        var animal = await _animalRepository.GetByIdAsync(animalId);
        if (animal == null) {
            throw new InvalidOperationException("Animal not found");
        }

        var newEnclosure = await _enclosureRepository.GetByIdAsync(newEnclosureId);
        if (newEnclosure == null)
        {
            throw new InvalidOperationException("New Enclosure not found");
        }

        var currentEnclosure = await _enclosureRepository.GetByIdAsync(animal.EnclosureId);
        if (currentEnclosure == null)
        {
            throw new InvalidOperationException("Current Enclosure not found");
        }

        
        currentEnclosure.RemoveAnimal(animal.Id);
        newEnclosure.AddAnimal(animal.Id);
        animal.MoveToEnclosure(newEnclosure.Id);

        await _animalRepository.UpdateAsync(animal);
        await _enclosureRepository.UpdateAsync(newEnclosure);
        await _enclosureRepository.UpdateAsync(currentEnclosure);

        // _eventRepository.Publish(new AnimalMovedEvent(animalId, oldEnclosure.Id, newEnclosure.Id));
    }
    public async Task<Guid> AddEnclosureAsync(string type, string size, int maxAnimals)
    {
        var enclosure = _enclosureFactory.Create(type, size, maxAnimals);
        await _enclosureRepository.AddAsync(enclosure);
        return enclosure.Id;
    }

    public async Task RemoveEnclosureAsync(Guid id)
    {
        await _enclosureRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EnclosureDto>> GetAllEnclosuresAsync()
    {
        var enclosure = await _enclosureRepository.GetAllAsync();
        return enclosure.Select(e => new EnclosureDto
        {
            Id = e.Id,
            Type = e.Type.ToString(),
            Size = e.Size,
            CurrentAnimals = e.CurrentAnimals,
            MaxAnimals = e.MaxAnimals,
            Animals = e.Animals,
        });

    }

    public async Task<EnclosureDto> GetEnclosureByIdAsync(Guid id)
    {
        var enclosure = await _enclosureRepository.GetByIdAsync(id);
        if (enclosure == null) return null;
        
        return new EnclosureDto
        {
            Id = enclosure.Id,
            Type = enclosure.Type.ToString(),
            Size = enclosure.Size,
            CurrentAnimals = enclosure.CurrentAnimals,
            MaxAnimals = enclosure.MaxAnimals,
            Animals = enclosure.Animals,
        };
    }

}