using AutoMapper;
using DressForWeather.SharedModels.Inputs;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using DressForWeather.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
public class DressReportController : ControllerBaseWithRouteToController
{
	private readonly MainDbContext _mainDbContext;
	private readonly IMapper _mapper;

	public DressReportController(MainDbContext mainDbContext, IMapper mapper)
	{
		_mainDbContext = mainDbContext;
		_mapper = mapper;
	}

	/// <summary>
	///     Добавить отчет о комфорте в определенной одежде в определенную погоду
	/// </summary>
	/// <param name="inputDressReport">Отчет</param>
	/// <returns>Идентификатор отчета</returns>
	[HttpPost]
	[ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
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

		await _mainDbContext.SaveChangesAsync();

		return dressReport.Entity.Id;
	}

	/// <summary>
	///     Получить отчет о комфорте в определенной одежде в определенную погоду
	/// </summary>
	/// <param name="id">Идентификатор отчета</param>
	/// <returns>Результат поиска отчета</returns>
	[HttpGet]
	[ProducesResponseType(typeof(OutputSearchResult<DressReport>), StatusCodes.Status200OK)]
	public async Task<OutputSearchResult<OutputDressReport>> Get(long id)
	{
		var report = await _mainDbContext.DressReports.Include(dr => dr.Clothes)
			.Include(dr => dr.WeatherState)
			.FirstOrDefaultAsync(dr => dr.Id == id);

		if (report is null)
			return new OutputSearchResult<OutputDressReport>(null);


		return new OutputSearchResult<OutputDressReport>(_mapper.Map<OutputDressReport>(report));
	}
}