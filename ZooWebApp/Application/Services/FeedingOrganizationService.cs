using System;
using ZooWebApp.Domain.Models;
using ZooWebApp.Domain.ValueObjects;
using ZooWebApp.Application.Interfaces;
namespace ZooWebApp.Application.Services;
public class FeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _feedingScheduleRepository;
    // private readonly IEventRepository _eventRepository;

    public FeedingOrganizationService(IFeedingScheduleRepository feedingScheduleRepository)
    {
        _feedingScheduleRepository = feedingScheduleRepository;
    }

    public async Task ScheduleFeeding(Guid animalId, DateTime feedingTime, Food foodType)
    {
        var schedule = new FeedingSchedule(animalId, feedingTime, foodType);
        await _feedingScheduleRepository.AddAsync(schedule);
        // _eventRepository.Publish(new FeedingTimeEvent(feedingSchedule.Id, animalId ,feedingTime));
    }

    public async Task CompleteFeeding(Guid scheduleId)
    {
        var schedule = await _feedingScheduleRepository.GetByIdAsync(scheduleId);
        if (schedule == null) {
            throw new ArgumentException("Feeding schedule not found");
        }

        schedule.MarkAsCompleted();
        await _feedingScheduleRepository.UpdateAsync(schedule);
    }
}