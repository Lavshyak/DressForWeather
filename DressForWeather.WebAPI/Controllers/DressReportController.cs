using System.Security.Claims;
using DressForWeather.SharedModels.Entities;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using DressForWeather.WebAPI.Exceptions;
using DressForWeather.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
public class DressReportController : ControllerBaseDressForWeather
{
	private readonly MainDbContext _mainDbContext;
	private readonly UserManager<User> _userManager;

	public DressReportController(MainDbContext mainDbContext, UserManager<User> userManager)
	{
		_mainDbContext = mainDbContext;
		_userManager = userManager;
	}

	[HttpPost]
	public async Task<long> AddDressReport(AddDressReportModel addDressReportModel)
	{
		var clotches = await _mainDbContext.Clotches.Where(c => addDressReportModel.ClothIds.Contains(c.Id))
			.ToListAsync();

		var dressReport = await _mainDbContext.DressReports.AddAsync(new DressReport
		{
			Clothes = clotches, Feeling = addDressReportModel.Feeling,
			UserReporter = await _mainDbContext.Users.FirstAsync(u => u.Id == User.GetId()),
			WeatherState =
				await _mainDbContext.WeatherStates.FirstAsync(w => w.Id == addDressReportModel.WeatherStateId)
		});

		return dressReport.Entity.Id;
	}

	[HttpGet]
	public async Task<DressReport?> GetDressReportById(long id)
	{
		var report = await _mainDbContext.DressReports.FirstOrDefaultAsync(dr => dr.Id == id);
		return report;
	}
}