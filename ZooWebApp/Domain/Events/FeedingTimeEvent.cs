namespace ZooWebApp.Domain.Events;
public class FeedingTimeEvent : IDomainEvent
{
    public Guid FeedingScheduleId { get;}
    public Guid AnimalId { get;}
    public DateTime FeedingTime { get; }

    public FeedingTimeEvent(Guid feedingScheduleId, Guid animalId, DateTime feedingTime)
    {
        FeedingScheduleId = feedingScheduleId;
        AnimalId = animalId;
        FeedingTime = feedingTime;
    }
}