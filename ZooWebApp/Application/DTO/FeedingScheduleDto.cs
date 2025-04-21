namespace ZooWebApp.Application.DTO;
public class FeedingScheduleDto
{
    public Guid Id { get; set; }
    public Guid AnimalId { get; set; }
    public DateTime FeedingTime { get; set; }
    public string FoodType { get; set; }
    public bool IsCompleted { get; set; }
}
