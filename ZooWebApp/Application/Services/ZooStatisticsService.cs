using ZooWebApp.Application.Interfaces;
namespace ZooWebApp.Application.Services;
public class ZooStatisticsService
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

        return new ZooStatistics
        {
            TotalAnimals = animals.Count(),
            HealthyAnimals = animals.Count(a => a.IsHealthy),
            SickAnimals = animals.Count(a => !a.IsHealthy),
            TotalEnclosures = enclosures.Count(),
            FreeEnclosures = enclosures.Count(e => e.CurrentAnimals == 0),
            AlmostFullEnclosures = enclosures.Count(e => e.CurrentAnimals >= e.MaxAnimals * 0.8)
        };
    }
}

public class ZooStatistics
{
    public int TotalAnimals { get; set; }
    public int HealthyAnimals { get; set; }
    public int SickAnimals { get; set; }
    public int TotalEnclosures { get; set; }
    public int FreeEnclosures { get; set; }
    public 