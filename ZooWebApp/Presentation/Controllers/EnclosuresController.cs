using Microsoft.AspNetCore.Mvc;
using ZooWebApp.Application.Interfaces;
using ZooWebApp.Infrastructure.Repositories;
using ZooWebApp.Domain.Models;
namespace ZooWebApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnclosuresController : ControllerBase
    {
        private readonly IEnclosureRepository _enclosureRepository;

        public EnclosuresController(IEnclosureRepository enclosureRepository)
        {
            _enclosureRepository = enclosureRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enclosures = await _enclosureRepository.GetAllAsync();
            return Ok(enclosures);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var enclosure = await _enclosureRepository.GetByIdAsync(id);
            if (enclosure == null) return NotFound();
            return Ok(enclosure);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnclosureRequest request)
        {
            var enclosure = new Enclosure(
                request.Type,
                request.Size,
                request.MaxAnimals);

            await _enclosureRepository.AddAsync(enclosure);
            return CreatedAtAction(nameof(GetById), new { id = enclosure.Id }, enclosure);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _enclosureRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("free")]
        public async Task<IActionResult> GetFreeEnclosures()
        {
            var enclosures = await _enclosureRepository.GetAllAsync();
            var freeEnclosures = enclosures.Where(e => e.CurrentAnimals < e.MaxAnimals);
            return Ok(freeEnclosures);
        }
    }

    public class CreateEnclosureRequest
    {
        public string Type { get; set; }
        public string Size { get; set; }
        public int MaxAnimals { get; set; }
    }
}