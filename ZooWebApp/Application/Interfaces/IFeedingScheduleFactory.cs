using ZooWebApp.Domain.Models;
namespace ZooWebApp.Application.Interfaces;
public interface IFeedingScheduleFactory{
    FeedingSchedule Create(Guid animalId, DateTime feedingTime, string food);
}