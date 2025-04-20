using ZooWebApp.Application.Interfaces;
using ZooWebApp.Application.DTO;

namespace ZooWebApp.Application.Services;

public class ZooStatisticsService : IZooStatisticsService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public ZooStatisticsService(
        IAnimalRepository animalRepository,
        IEnclosureRepository enclosureRepository)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
    }

    public async Task<ZooStatistics> GetStatistics()
    {
        var animals = await _animalRepository.GetAllAsync();
        var enclosures = await _enclosureRepository.GetAllAsync();

        var availableSpaceEnclosures = enclosures
            .Where(e => e.CurrentAnimals < e.MaxAnimals)
            .Select(e => new EnclosureInfo
            {
                Id = e.Id,
                CurrentAnimals = e.CurrentAnimals,
                MaxAnimals = e.MaxAnimals
            })
            .ToList();

        return new ZooStatistics
        {
            TotalAnimals = animals.Count(),
            HealthyAnimals = animals.Count(a => a.IsHealthy),
            SickAnimals = animals.Count(a => !a.IsHealthy),
            TotalEnclosures = enclosures.Count(),
            EnclosuresWithAvailableSpace = availableSpaceEnclosures,
        };
    }
}


