using DressForWeather.SharedModels.Inputs;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using DressForWeather.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
public class DressReportController : ControllerBaseWithRouteToController
{
	private readonly IMainDbContext _mainDbContext;
	private readonly UserManager<User> _userManager;

	public DressReportController(IMainDbContext mainDbContext, UserManager<User> userManager)
	{
		_mainDbContext = mainDbContext;
		_userManager = userManager;
	}

	[HttpPost]
	[ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
	public async Task<long> Set(InputDressReport inputDressReport)
	{
		var clotches = await _mainDbContext.Clotches.Where(c => inputDressReport.ClothIds.Contains(c.Id))
			.ToListAsync();

		var dressReport = await _mainDbContext.DressReports.AddAsync(new DressReport
		{
			Clothes = clotches, Feeling = inputDressReport.Feeling,
			UserReporter = await _mainDbContext.Users.FirstAsync(u => u.Id == User.GetId()),
			WeatherState =
				await _mainDbContext.WeatherStates.FirstAsync(w => w.Id == inputDressReport.WeatherStateId)
		});

		return dressReport.Entity.Id;
	}

	[HttpGet]
	[ProducesResponseType(typeof(OutputSearchResult<DressReport>), StatusCodes.Status200OK)]
	public async Task<OutputSearchResult<DressReport>> Get(long id)
	{
		var report = await _mainDbContext.DressReports.FirstOrDefaultAsync(dr => dr.Id == id);
		return new OutputSearchResult<DressReport>(report);
	}
}