using System;
using ZooWebApp.Domain.Models;
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

    public async Task ScheduleFeeding(Guid animalId, DateTime feedingTime, string foodType)
    {
        var schedule = new FeedingSchedule(animalId, feedingTime, foodType);
        await _feedingScheduleRepository.AddAsync(schedule);
        // _eventRepository.Publish(new FeedingTimeEvent(feedingSchedule.Id, animalId ,feedingTime));
    }

    public async Task CompleteFeeding(Guid scheduleId)
    {
        var schedule = await _feedingSchedule