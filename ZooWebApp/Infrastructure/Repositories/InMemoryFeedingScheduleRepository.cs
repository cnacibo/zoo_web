using ZooWebApp.Application.Interfaces;
using ZooWebApp.Domain.Models;
namespace ZooWebApp.Infrastructure.Repositories;
public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
    {
        private static readonly List<FeedingSchedule> _schedules = new();

        public Task<FeedingSchedule> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_schedules.FirstOrDefault(s => s.Id == id));
        }

        public Task<IEnumerable<FeedingSchedule>> GetAllAsync()
        {
            return Task.FromResult(_schedules.AsEnumerable());
        }

        public Task AddAsync(FeedingSchedule schedule)
        {
            _schedules.Add(schedule);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(FeedingSchedule schedule)
        {
            var index = _schedules.FindIndex(s => s.Id == schedule.Id);
            if (index >= 0)
            {
                _schedules[index] = schedule;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _schedules.RemoveAll(s => s.Id == id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<FeedingSchedule>> GetByAnimalIdAsync(Guid animalId)
        {
            return Task.FromResult(_schedules.Where(s => s.AnimalId == animalId));
        }
    }