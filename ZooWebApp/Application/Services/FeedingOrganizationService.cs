using ZooWebApp.Application.DTO;
using ZooWebApp.Application.Interfaces;

namespace ZooWebApp.Application.Services;
public class FeedingOrganizationService : IFeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _feedingScheduleRepository;
    private readonly IFeedingScheduleFactory _feedingScheduleFactory;
    private readonly IAnimalRepository  _animalRepository; 
    private readonly IEventRepository _eventRepository;

    public FeedingOrganizationService(IFeedingScheduleRepository feedingScheduleRepository, 
    IFeedingScheduleFactory feedingScheduleFactory, IAnimalRepository animalRepository, IEventRepository eventRepository)
    {
        _feedingScheduleRepository = feedingScheduleRepository;
        _feedingScheduleFactory = feedingScheduleFactory;
        _animalRepository = animalRepository;
        _eventRepository = eventRepository;
    }

    public async Task<Guid> CreateFeedingScheduleAsync(Guid animalId, DateTime feedingTime, string food)
    {
        var animal = await _animalRepository.GetByIdAsync(animalId);
        if (animal == null){
            throw new InvalidOperationException("Animal not found");
        }

        var schedule = _feedingScheduleFactory.Create(animalId, feedingTime, food);
        await _feedingScheduleRepository.AddAsync(schedule);
        return schedule.Id;
    }

    public async Task CompleteFeedingAsync(Guid scheduleId){
        var schedule = await _feedingScheduleRepository.GetByIdAsync(scheduleId);
        if (schedule == null){
            throw new InvalidOperationException("Feeding schedule not found");
        }
        schedule.MarkAsCompleted();
        await _feedingScheduleRepository.UpdateAsync(schedule);
    }

    public async Task DeleteFeedingScheduleAsync(Guid scheduleId){
        var schedule = await _feedingScheduleRepository.GetByIdAsync(scheduleId);
        if (schedule == null){
            throw new InvalidOperationException("Feeding schedule not found");
        }
        await _feedingScheduleRepository.DeleteAsync(scheduleId);
    }

    public async Task<IEnumerable<FeedingScheduleDto>> GetAllFeedingSchedulesAsync()
    {
        var schedules = await _feedingScheduleRepository.GetAllAsync();
        return schedules.Select(s => new FeedingScheduleDto
        {
            Id = s.Id,
            AnimalId = s.AnimalId,
            FeedingTime = s.FeedingTime,
            FoodType = s.FoodType.ToString(),
            IsCompleted = s.IsCompleted
        });
    }

}