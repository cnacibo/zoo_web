namespace ZooWebApp.Domain.Models;
public class FeedingSchedule
{
    public Guid Id { get; private set; }
    public Guid AnimalId { get; private set; }
    public DateTime FeedingTime { get; private set; }
    public string FoodType { get; private set; }
    public bool IsCompleted { get; private set; }

    public FeedingSchedule(Guid animalId, DateTime feedingTime, string foodType)
    {
        Id = Guid.NewGuid();
        AnimalId = animalId;
        FeedingTime = feedingTime;
        FoodType = foodType;
        IsCompleted = false;
    }

    public void UpdateSchedule(DateTime newTime, string newFoodType)
    {
        FeedingTime = newTime;
        FoodType = newFoodType;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
        // DomainEvents.Raise(new FeedingTimeEvent(this));
    }
}