using Microsoft.VisualBasic;
using ZooWebApp.Application.Interfaces;
// using ZooWebApp.Domain.Models;
namespace ZooWebApp.Application.Services;
public class AnimalTransferService : IAnimalTransferService
{
    private readonly IAnimalRepository  _animalRepository; 
    private readonly IEnclosureRepository _enclosureRepository;
    private readonly IAnimalFactory _animalFactory;

    public AnimalTransferService( IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository, IAnimalFactory animalFactory)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
        _animalFactory = animalFactory;
    }

    public async Task AddAnimalAsync(string species, string name, DateTime birthDate, 
        string gender, string favoriteFood, Guid enclosureId)
    {

        var enclosure = await _enclosureRepository.GetByIdAsync(enclosureId);
        if (enclosure == null)
            throw new InvalidOperationException("Enclosure not found");

        var animal = _animalFactory.Create(species, name, birthDate, gender, favoriteFood, enclosureId);

        enclosure.AddAnimal(animal.Id);
        await _animalRepository.AddAsync(animal);
        await _enclosureRepository.UpdateAsync(enclosure);
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

}