using Microsoft.AspNetCore.Mvc;
using ZooWebApp.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ZooWebApp.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FeedingSchedulesController : ControllerBase
{
    private readonly IFeedingOrganizationService _feedingService;

    public FeedingSchedulesController(IFeedingOrganizationService feedingOrganizationService)
    {
        _feedingService = feedingOrganizationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var schedules = await _feedingService.GetAllFeedingSchedulesAsync();
            return Ok(schedules);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateFeedingScheduleRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await _feedingService.CreateFeedingScheduleAsync(request.AnimalId, request.FeedingTime, request.FoodType);
            return Ok(new {Message = "Feeding Schedule successfully updated", Id = id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id){
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _feedingService.CompleteFeedingAsync(id);
            return Ok("Feeding marked as completed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id){
        try
        {
            await _feedingService.DeleteFeedingScheduleAsync(id);
            return Ok("Feeding schedule deleted");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class CreateFeedingScheduleRequest
{
    [Required]
    public Guid AnimalId { get; set; }

    [Required]
    public DateTime FeedingTime { get; set; }

    [Required]
    public string FoodType { get; set; }
}

public class UpdateFeedingScheduleRequest
{
    [Required]
    public DateTime FeedingTime { get; set; }

    [Required(ErrorMessage = "FoodType is required: Meat, Fish, Grass, Vegetables, Fruit")]
    public string FoodType { get; set; }
}