using Microsoft.AspNetCore.Mvc;
using ZooWebApp.Application.Services;
using ZooWebApp.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using ZooWebApp.Presentation.DTO;

using ZooWebApp.Application.Interfaces;

namespace ZooWebApp.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly AnimalTransferService _animalTransferService;
    private readonly IAnimalRepository _animalRepository;
    
    public AnimalsController(AnimalTransferService animalTransferService, IAnimalRepository animalRepository)
    {
        _animalTransferService = animalTransferService;
         _animalRepository = animalRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var animals = await _animalRepository.GetAllAsync();
        return Ok(animals);
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetById(Guid id)
    // {
    //     var animal = await _animalRepository.GetByIdAsync(id);
    //     if (animal == null) return NotFound();
    //     return Ok(animal);
    // }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateAnimalRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _animalTransferService.AddAnimalAsync(request.Species, request.Name, request.BirthDate, request.Gender, request.FavoriteFood, request.EnclosureId);
            return Ok("Animal successfully added");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); // ошибки от фабрики (enum парсинг)
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message); // не найден вольер
        }
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferAnimal([FromForm] TransferAnimalRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _animalTransferService.TransferAnimal(request.AnimalId, request.NewEnclosureId);
            return Ok("Animal successfully transferred");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message); // не найдено животное или вольер
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _animalTransferService.RemoveAnimalAsync(id);
            return NoContent(); // успешно
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message); // если животное или вольер не найдены
        }
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

public class TransferAnimalRequest
{
    [Required]
    public Guid AnimalId { get; set; }

    [Required]
    public Guid NewEnclosureId { get; set; }
}
