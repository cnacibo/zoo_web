using ZooWebApp.Application.Interfaces;
using ZooWebApp.Domain.Models;
namespace ZooWebApp.Infrastructure.Repositories;
public class InMemoryEnclosureRepository : IEnclosureRepository{
    private static readonly List<Enclosure> _enclosures = new();

    public Task<Enclosure> GetByIdAsync(Guid id){
        return Task.FromResult(_enclosures.FirstOrDefault(e => e.Id == id));
    }

    public Task<IEnumerable<Enclosure>> GetAllAsync() {
        return Task.FromResult(_enclosures.AsEnumerable());
    }

    public Task AddAsync(Enclosure enclosure) {
        _enclosures.Add(enclosure);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Enclosure enclosure) {
        var index = _enclosures.FindIndex(e => e.Id == enclosure.Id);
        if (index >= 0) {
            _enclosures[index] = enclosure;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id) {
        _enclosures.RemoveAll(e => e.Id == id);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid enclosureId)
    {
        return Task.FromResult(_enclosures.Any(e => e.Id == enclosureId));
    }
}