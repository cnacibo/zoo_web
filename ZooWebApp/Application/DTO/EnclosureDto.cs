namespace ZooWebApp.Application.DTO;
public class EnclosureDto
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Size { get; set; }
    public int CurrentAnimals { get; set; }
    public int MaxAnimals { get; set; }
    public List<Guid> Animals { get; set; }
}
