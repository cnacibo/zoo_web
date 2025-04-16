using Microsoft.AspNetCore.Mvc;
using ZooWebApp.Application.Interfaces;
using ZooWebApp.Application.Services;
using ZooWebApp.Infrastructure.Repositories;

namespace ZooWebApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedingSchedulesController : ControllerBase
    {
        private readonly IFeedingScheduleRepository _feedingScheduleRepository;
        private readonly FeedingOrganizationService _feedingOrganizationService;

        public FeedingSchedulesController(
            IFeedingScheduleRepository feedingScheduleRepository,
            FeedingOrganizationService feedingOrganizationService)
        {
            _feedingScheduleRepository = feedingScheduleRepository;
            _feedingOrganizationService = feedingOrganizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schedules = await _feedingScheduleRepository.GetAllAsync();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var schedule = await _feedingScheduleRepository.GetByIdAsync(id);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }

        [HttpGet("animal/{animalId}")]
        public async Task<IActionResult> GetByAnimalId(Guid animalId)
        {
            var schedules = await _feedingScheduleRepository.GetByAnimalIdAsync(animalId);
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeedingScheduleRequest request)
        {
            await _feedingOrganizationService.ScheduleFeeding(
                request.AnimalId,
                request.FeedingTime,
                request.FoodType);

            return Ok();
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteFeeding(Guid id)
        {
            await _feedingOrganizationService.CompleteFeeding(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFeedingScheduleRequest request)
        {
            var schedule = await _feedingScheduleRepository.GetByIdAsync(id);
            if (schedule == null) return NotFound();

            schedule.UpdateSchedule(request.FeedingTime, request.FoodType);
            await _feedingScheduleRepository.UpdateAsync(schedule);

            return Ok(schedule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _feedingScheduleRepository.DeleteAsync(id);
            return NoContent();
        }
    }

    public class CreateFeedingScheduleRequest
    {
        public Guid AnimalId { get; set; }
        public DateTime FeedingTime { get; set; }
        public string FoodType { get; set; }
    }

    public class UpdateFeedingScheduleRequest
    {
        public DateTime FeedingTime { get; set; }
        public string FoodType { get; set; }
    }
}