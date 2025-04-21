using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ZooWebApp.Application.Interfaces;

namespace ZooWebApp.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalTransferService _animalTransferService;
    
    public AnimalsController(IAnimalTransferService animalTransferService)
    {
        _animalTransferService = animalTransferService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var animals = await _animalTransferService.GetAllAnimalsAsync();
            return Ok(animals);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var animal = await _animalTransferService.GetAnimalByIdAsync(id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateAnimalRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await _animalTransferService.AddAnimalAsync(request.Species, request.Name, request.BirthDate, request.Gender, request.FavoriteFood, request.EnclosureId);
            return Ok(new{Message = "Animal successfully added", Id = id});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // ошибки 
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
        catch (Exception ex)
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
        catch (Exception ex)
        {
            return NotFound(ex.Message); // если животное или вольер не найдены
        }
    }
}

public class CreateAnimalRequest
{
    [Required]
    public string Species { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    public string FavoriteFood { get; set; }

    [Required]
    public Guid EnclosureId { get; set; }
}

public class TransferAnimalRequest
{
    [Required]
    public Guid AnimalId { get; set; }

    [Required]
    public Guid NewEnclosureId { get; set; }
}
