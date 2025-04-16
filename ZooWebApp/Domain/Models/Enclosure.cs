namespace ZooWebApp.Domain.Models;
public class Enclosure
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public int Size { get; private set; }
    public int CurrentAnimals { get; private set; }
    public int MaxAnimals { get; private set; }
    public List<Guid> Animals { get; private set; }

    public Enclosure(string type, int size, int currentAnimals, int maxAnimals)
    {
        Id = Guid.NewGuid();
        Type = type;
        Size = size;
        MaxAnimals = maxAnimals;
        // CurrentAnimals = currentAnimals;
        CurrentAnimals = 0;
        Animals = new List<Guid>();
    }

    public bool CanAddAnimal()
    {
        return CurrentAnimals < MaxAnimals;
    }

    public void AddAnimal(Guid animalId)
    {
        if (!CanAddAnimal()) {
            throw new InvalidOperationException("Enclosure is full");
        }
        Animals.Add(animalId);
        CurrentAnimals++;
    }

    public void RemoveAnimal(Guid animalId)
    {
        if (CurrentAnimals <= 0) {
            throw new InvalidOperationException("No animals in enclosure");
        }
        if (!Animals.Contains(animalId)){
            throw new Inv