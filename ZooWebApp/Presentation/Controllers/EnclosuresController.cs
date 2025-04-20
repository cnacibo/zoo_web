using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace ZooWebApp.Presentation.Controllers;

using ZooWebApp.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IAnimalTransferService _animalTransferService;
    private readonly IEnclosureRepository _enclosureRepository;

    public EnclosuresController(IAnimalTransferService animalTransferService, IEnclosureRepository enclosureRepository)
    {
        _animalTransferService = animalTransferService;   
        _enclosureRepository = enclosureRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enclosures = await _enclosureRepository.GetAllAsync();
        return Ok(enclosures);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateEnclosureRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _animalTransferService.AddEnclosureAsync(request.Type, request.Size, request.MaxAnimals);
            return Ok("Enclosure successfully created");
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