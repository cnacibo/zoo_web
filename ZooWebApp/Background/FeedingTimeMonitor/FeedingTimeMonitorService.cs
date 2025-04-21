using Microsoft.Extensions.Hosting;
using ZooWebApp.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZooWebApp.Domain.Events;

namespace ZooWebApp.Background.FeedingTimeMonitor;
public class FeedingTimeMonitorService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30); 

    public FeedingTimeMonitorService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var feedingRepo = scope.ServiceProvider.GetRequiredService<IFeedingScheduleRepository>();
            var animalRepo = scope.ServiceProvider.GetRequiredService<IAnimalRepository>();
            var eventRepo = scope.ServiceProvider.GetRequiredService<IEventRepository>();

            var allSchedules = await feedingRepo.GetAllAsync();
            var dueSchedules = allSchedules
                .Where(s => s.FeedingTime <= DateTime.UtcNow && !s.IsCompleted)
                .ToList();

            foreach (var schedule in dueSchedules)
            {
        
                eventRepo.Publish(new FeedingTimeEvent(schedule.Id, schedule.AnimalId, schedule.FeedingTime));

            
                schedule.MarkAsCompleted();
                await feedingRepo.UpdateAsync(schedule);
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }
}
