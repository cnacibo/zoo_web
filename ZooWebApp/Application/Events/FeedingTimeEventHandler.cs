using ZooWebApp.Domain.Events;
using ZooWebApp.Application.Interfaces;
namespace ZooWebApp.Application.Events;
public class FeedingTimeEventHandler : IEventHandler<FeedingTimeEvent>
{
    private readonly IAnimalRepository _animalRepository;

    public FeedingTimeEventHandler(IAnimalRepository animalRepository) {
        _animalRepository = animalRepository;

    }
    public async Task Handle(FeedingTimeEvent @event)
    {
        var animal = await _animalRepository.GetByIdAsync(@event.AnimalId);
        Console.WriteLine($"Time to feed animal {animal.Name} with id {@animal.Id}!");
        await Task.CompletedTask;
        
    }
}
