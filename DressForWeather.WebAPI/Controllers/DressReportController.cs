using DressForWeather.DbContextTemplates;
using DressForWeather.Models.EFCoreModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DressReportController : ControllerBase
{
	private readonly ILogger<DressReportController> _logger;
	private readonly WeatherDressDbContext _weatherDressDbContext;
	public DressReportController(ILogger<DressReportController> logger, WeatherDressDbContext weatherDressDbContext)
	{
		_logger = logger;
		_weatherDressDbContext = weatherDressDbContext;
	}

	[HttpGet(Name = "GetDressReportById")]
	public async Task<DressReport?> Get(long id)
	{
		var report = await _weatherDressDbContext.DressReports.FirstOrDefaultAsync(dr => dr.Id == id);
		return report;
	}
}