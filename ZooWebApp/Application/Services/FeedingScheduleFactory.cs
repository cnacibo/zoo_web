using ZooWebApp.Domain.Models;
using ZooWebApp.Domain.ValueObjects;
using ZooWebApp.Application.Interfaces;
namespace ZooWebApp.Application.Services;
public class FeedingScheduleFactory : IFeedingScheduleFactory{
    public FeedingSchedule Create(Guid animalId, DateTime feedingTime, string foodInput){
        if (!Enum.TryParse<Food>(foodInput, true, out var food)){
            throw new ArgumentException("Invalid food type: Meat, Fish, Grass, Vegetables, Fruit");
        }
        return new FeedingSchedule(animalId, feedingTime, food);
    }
}