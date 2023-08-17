using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[ApiController]
public class DressReportController : ControllerBaseDressForWeather
{
	private readonly ILogger<DressReportController> _logger;
	private readonly MainDbContext _mainDbContext;
	public DressReportController(ILogger<DressReportController> logger, MainDbContext mainDbContext)
	{
		_logger = logger;
		_mainDbContext = mainDbContext;
	}

	[Authorize]
	[HttpGet]
	public async Task<DressReport?> GetDressReportById(long id)
	{
		var report = await _mainDbContext.DressReports.FirstOrDefaultAsync(dr => dr.Id == id);
		return report;
	}
}