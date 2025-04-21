using ZooWebApp.Domain.Events;
namespace ZooWebApp.Application.EventHandlers;
public class FeedingTimeEventHandler
{
    public void Handle(FeedingTimeEvent feedingTimeEvent)
    {
        Console.WriteLine($"Time to feed animal {feedingTimeEvent.AnimalId}!");
        
    }
}
