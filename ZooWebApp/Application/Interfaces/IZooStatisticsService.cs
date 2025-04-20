using ZooWebApp.Application.DTO;
namespace ZooWebApp.Application.Interfaces;

public interface IZooStatisticsService
{
    Task<ZooStatistics> GetStatistics();
}