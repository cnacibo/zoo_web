using ZooWebApp.Domain.Models;
namespace ZooWebApp.Domain.Events;
public class FeedingTimeEvent : IDomainEvent
{
    public FeedingSchedule FeedingSchedule { get; }

    public FeedingTimeEvent(FeedingSchedule feedingSchedule)
    {
        FeedingSchedule = feedingSchedule;
    }
}