using DressForWeather.DbContextTemplates;
using DressForWeather.Models.EFCoreModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DressForWeather.Models.EFCoreModels.DressRepairModels;
namespace DressForWeather.WebAPI.Controllers;

// TODO: дописать контроллер, написать недостающие контроллеры
[ApiController]
[Route("[controller]")]
public class DressReportController : ControllerBase
{
	private readonly ILogger<DressReportController> _logger;
	private readonly WeatherDressDbContext _weatherDressDbContext;
	private IDressRepos DressRepos { get; set; }
	public DressReportController(ILogger<DressReportController> logger, WeatherDressDbContext weatherDressDbContext,IDressRepos dressRepos)
	{
		_logger = logger;
		_weatherDressDbContext = weatherDressDbContext;
		DressRepos = dressRepos;
	}

	[HttpGet(Name = "GetAllDressReport")]
	public async List<Task<DressReport>> GetAll() 
	{
		var repots = await DressRepos.GetAllDressReport();
		return repots;

	}
	[HttpGet(Name = "GetDressReportById")]
	public async Task<DressReport?> Get(long id)
	{
		var report = await _weatherDressDbContext.DressReports.FirstOrDefaultAsync(dr => dr.Id == id);
		return report;
	}
	[HttPost(Name = "CreateDressReport")]
	public async Task<DressReport> Create(DressReport model) 
	{
		var report = await DressRepos.Create(model);
		return report; 
	}
	[HttpPut(Name = "UpdateDressReport")]
	public async Task<DressReport> Update(int id, DressReport model) 
	{
		bool done = false;
		var report = await GetDressById(id);
		if(report != null) 
		{
			report = DressRepos.Update(model);
			done = true;
		}
		return done ? report : null;
	}
}