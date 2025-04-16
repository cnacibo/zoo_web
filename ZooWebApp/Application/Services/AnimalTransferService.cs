using ZooWebApp.Application.Interfaces;
using ZooWebApp.Domain.Models;
namespace ZooWebApp.Application.Services;
public class AnimalTransferService
{
    private readonly IAnimalRepository  _animalRepository; 
    private readonly IEnclosureRepository _enclosureRepository;

    public AnimalTransferService(
        IAnimalRepository animalRepository,
        IEnclosureRepository enclosureRepository)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
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