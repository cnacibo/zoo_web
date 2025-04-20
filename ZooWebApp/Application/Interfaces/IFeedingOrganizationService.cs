namespace ZooWebApp.Application.Interfaces;

public interface IFeedingOrganizationService
{
    Task<Guid> CreateFeedingScheduleAsync(Guid animalId, DateTime feedingTime, string food);

    Task CompleteFeedingAsync(Guid scheduleId);

    Task DeleteFeedingScheduleAsync(Guid scheduleId);
}
