using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace ZooWebApp.Presentation.Controllers;

using ZooWebApp.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IAnimalTransferService _animalTransferService;

    public EnclosuresController(IAnimalTransferService animalTransferService)
    {
        _animalTransferService = animalTransferService;   
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var enclosures = await _animalTransferService.GetAllEnclosuresAsync();
        return Ok(enclosures);
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
            var enclosure = await _animalTransferService.GetEnclosureByIdAsync(id);
        if (enclosure == null) return NotFound();
        return Ok(enclosure);
        }
        catch (Exception ex){
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateEnclosureRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await _animalTransferService.AddEnclosureAsync(request.Type, request.Size, request.MaxAnimals);
            return Ok(new{Message = "Enclosure successfully created", Id = id});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try{
            await _animalTransferService.RemoveEnclosureAsync(id);
            return Ok("Enclosure successfully deleted");
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
        
    }
}

public class CreateEnclosureRequest{
    [Required]
    public string Type { get; set; }

    [Required]
    public string Size { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Max animals must be at least 1")]
    public int MaxAnimals { get; set; }
}