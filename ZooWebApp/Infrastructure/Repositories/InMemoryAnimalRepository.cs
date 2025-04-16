using ZooWebApp.Application.Interfaces;
using ZooWebApp.Domain.Models;
namespace ZooWebApp.Infrastructure.Repositories;
public class InMemoryAnimalRepository : IAnimalRepository
{
    private static readonly List<Animal> _animals = new();

    public Task<Animal> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_animals.FirstOrDefault(a => a.Id == id));
    }

    public Task<IEnumerable<Animal>> GetAllAsync()
    {
        return Task.FromResult(_animals.AsEnumerable());
    }

    public Task AddAsync(Animal animal)
    {
        _animals.Add(animal);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Animal animal)
    {
        var index = _animals.FindIndex(a => a.Id == animal.Id);
        if (index >= 0)
        {
            _animals[index] = animal;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _animals.RemoveAll(a => a.Id == 