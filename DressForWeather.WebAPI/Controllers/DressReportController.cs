using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

// TODO: дописать контроллер, написать недостающие контроллеры
[ApiController]
[Route("[controller]")]
[Authorize]
public class DressReportController : ControllerBase
{
	private readonly ILogger<DressReportController> _logger;
	private readonly MainDbContext _mainDbContext;
	public DressReportController(ILogger<DressReportController> logger, MainDbContext mainDbContext)
	{
		_logger = logger;
		_mainDbContext = mainDbContext;
	}

	
	[HttpGet(Name = "GetDressReportById")]
	public async Task<DressReport?> Get(long id)
	{
		var report = await _mainDbContext.DressReports.FirstOrDefaultAsync(dr => dr.Id == id);
		return report;
	}
}