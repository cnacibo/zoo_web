using Microsoft.AspNetCore.Mvc;
using ZooWebApp.Application.Interfaces;

namespace ZooWebApp.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZooStatisticsController : ControllerBase
{
    private readonly IZooStatisticsService _zooStatisticsService;

    public ZooStatisticsController(IZooStatisticsService zooStatisticsService)
    {
        _zooStatisticsService = zooStatisticsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStatistics()
    {
        try
        {
            var statistics = await _zooStatisticsService.GetStatistics();
            return Ok(statistics);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to retrieve statistics: {ex.Message}");
        }
    }
}
