using ZooWebApp.Domain.Events;
using ZooWebApp.Application.Interfaces;
namespace ZooWebApp.Application.Events;
public class AnimalMovedEventHandler : IEventHandler<AnimalMovedEvent>
{

    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public AnimalMovedEventHandler(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository) {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;

    }
    

    public async Task Handle(AnimalMovedEvent @event)
    {

        var animal = await _animalRepository.GetByIdAsync(@event.AnimalId);
        var currEnclosure = await _enclosureRepository.GetByIdAsync(@event.CurrentEnclosureId);
        var newEnclosure = await _enclosureRepository.GetByIdAsync(@event.NewEnclosureId);
        Console.WriteLine($"Animal {animal.Name} moved from {currEnclosure.Type} enclosure {@currEnclosure.Id} to {newEnclosure.Type} enclosure {@newEnclosure.Id}");
        await Task.CompletedTask; 
    }
}