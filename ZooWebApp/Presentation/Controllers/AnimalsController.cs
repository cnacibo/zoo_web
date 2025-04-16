using Microsoft.AspNetCore.Mvc;
using ZooWebApp.Application.Interfaces;
using ZooWebApp.Infrastructure.Repositories;
using ZooWebApp.Domain.Models;
namespace ZooWebApp.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalsController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var animals = await _animalRepository.GetAllAsync();
        return Ok(animals);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var animal = await _animalRepository.GetByIdAsync(id);
        if (animal == null) return NotFound();
        return Ok(animal);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAnimalRequest request)
    {
        var animal = new Animal(
            // new AnimalSpecies(request.Species),
            // new AnimalName(request.Name),
            request.Species,
            request.Name,
            request.BirthDate,
            request.Gender,
            request.FavoriteFood,
            request.EnclosureId);

        await _animalRepository.AddAsync(animal);
        return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _animalRepository.DeleteAsync(id);
        return NoContent();
    }
}

public class CreateAnimalRequest
{
    public string Species { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    public string FavoriteFood { get; set; }
    public Guid EnclosureId { get; set; }
}