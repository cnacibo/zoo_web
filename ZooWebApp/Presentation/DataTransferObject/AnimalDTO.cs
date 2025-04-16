using ZooWebApp.Domain.ValueObjects;
namespace ZooWebApp.Presentation.DTO;
public class AnimalDTO
{
    public Guid Id { get; private set; }
    public string Species { get; private set; }
    public string Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public Food FavoriteFood { get; private set; }
    public bool IsHealthy { get; private set; }
    public Guid EnclosureId { get; private set; }
    
}