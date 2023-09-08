using AutoMapper;
using DressForWeather.SharedModels.Inputs;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ClothParameterPairController : ControllerBaseWithRouteToController
{
	private readonly MainDbContext _dbContext;
	private readonly IMapper _mapper;

	public ClothParameterPairController(MainDbContext db, IMapper mapper)
	{
		_dbContext = db;
		_mapper = mapper;
	}

	/// <summary>
	///     Добавляет предмет одежды в базу данных
	/// </summary>
	/// <param name="inputClothParameterPair">Информация о предмете одежды</param>
	/// <returns>Id добавленной одежды</returns>
	[HttpPost]
	[ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
	public async Task<long> Set(InputClothParameterPair inputClothParameterPair)
	{
		var clotchParameterPair = await _dbContext.ClotchParameterPairs.AddAsync(new ClothParameterPair
			{Key = inputClothParameterPair.Key, Value = inputClothParameterPair.Value});
		await _dbContext.SaveChangesAsync();
		return clotchParameterPair.Entity.Id;
	}

	/// <summary>
	///     Получить первую попавшуюся информацию о предмете одежды
	/// </summary>
	/// <param name="id">если указано, ищет по идентификатору</param>
	/// <param name="key">если указано, ищет по ключу</param>
	/// <param name="value">если указано, ищет по значению</param>
	/// <returns>Результат поиска. Если найдено, то в нем будет информация о предмете одежды</returns>
	[HttpGet]
	[ProducesResponseType(typeof(OutputSearchResult<OutputClothParameterPair>), StatusCodes.Status200OK)]
	public async Task<OutputSearchResult<OutputClothParameterPair>> Get([FromQuery] long? id = null,
		[FromQuery] string? key = null,
		[FromQuery] string? value = null)
	{
		ClothParameterPair? clotchParameterPair = null;
		if (id is not null)
			clotchParameterPair = await _dbContext.ClotchParameterPairs.FirstOrDefaultAsync(c => c.Id == id);
		else if (key is not null)
			clotchParameterPair = await _dbContext.ClotchParameterPairs.FirstOrDefaultAsync(c => c.Key == key);
		else if (value is not null)
			clotchParameterPair = await _dbContext.ClotchParameterPairs.FirstOrDefaultAsync(c => c.Value == value);

		return clotchParameterPair is null
			? new OutputSearchResult<OutputClothParameterPair>(null)
			: new OutputSearchResult<OutputClothParameterPair>(
				_mapper.Map<OutputClothParameterPair>(clotchParameterPair));
	}
}