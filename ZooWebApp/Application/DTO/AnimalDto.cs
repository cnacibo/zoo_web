namespace ZooWebApp.Application.DTO;
public class AnimalDto
{
    public Guid Id { get; set; }
    public string Species { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    public string FavoriteFood { get; set; }
    public bool IsHealthy { get; set; }
    public Guid EnclosureId { get; set; }
}
