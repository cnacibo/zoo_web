using Microsoft.AspNetCore.Mvc;
using ZooWebApp.Application.Interfaces;
using ZooWebApp.Infrastructure.Repositories;
using ZooWebApp.Domain.Models;
using ZooWebApp.Domain.ValueObjects;
using ZooWebApp.Application.Commands;
using ZooWebApp.Presentation.DTO;
using System.ComponentModel.DataAnnotations;

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
    public async Task<IActionResult> Create([FromForm] CreateAnimalRequest request)
    {
        // var command = new AddAnimalCommand(
        //     dto.Species,
        //     dto.Name,
        //     dto.BirthDate,
        //     dto.Gender,
        //     dto.FavoriteFood,
        //     dto.EnclosureId
        //     );
        if (!Enum.TryParse<Gender>(request.Gender, out var gender))
        {
            return BadRequest("Invalid gender value");
        }

        if (!Enum.TryParse<Food>(request.FavoriteFood, out var food))
        {
            return BadRequest("Invalid favorite food value");
        }
        var animal = new Animal(
            // new AnimalSpecies(request.Species),
            // new AnimalName(request.Name),
            request.Species,
            request.Name,
            request.BirthDate,
            gender,
            food,
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
    [Required(ErrorMessage = "Species is required")]
    public string Species { get; set; }

     [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "BirthDate is required")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    [EnumDataType(typeof(Gender), ErrorMessage = "Invalid Gender")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "FavoriteFood is required")]
    [EnumDataType(typeof(Food), ErrorMessage = "Invalid FavoriteFood")]
    public string FavoriteFood { get; set; }

    [Required(ErrorMessage = "EnclosureId is required")]
    public Guid EnclosureId { get; set; }
}
