using ZooWebApp.Domain.Models;
namespace ZooWebApp.Application.Interfaces;
public interface IFeedingScheduleRepository
{
    Task<FeedingSchedule> GetByIdAsync(Guid id);
    Task<IEnumerable<FeedingSchedule>> GetAllAsync();
    Task AddAsync(FeedingSchedule schedule);
    Task UpdateAsync(FeedingSchedule schedule);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<FeedingSchedule>> GetByAnimalIdAsync(Guid animalId);
}