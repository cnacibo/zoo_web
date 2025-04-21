namespace ZooWebApp.Application.DTO;
public class ZooStatistics
{
    public int TotalAnimals { get; set; }
    public int HealthyAnimals { get; set; }
    public int SickAnimals { get; set; }
    public int TotalEnclosures { get; set; }

    public List<EnclosureInfo> EnclosuresWithAvailableSpace { get; set; } = new();
}
