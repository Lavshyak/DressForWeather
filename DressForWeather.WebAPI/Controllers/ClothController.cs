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
public class ClothController : ControllerBaseWithRouteToController
{
	private readonly MainDbContext _dbContext;
	private readonly IMapper _mapper;

	public ClothController(MainDbContext db, IMapper mapper)
	{
		_dbContext = db;
		_mapper = mapper;
	}

	/// <summary>
	///     Добавляет предмет одежды в базу данных
	/// </summary>
	/// <param name="inputCloth">Информация о предмете одежды</param>
	/// <returns>Id добавленной одежды</returns>
	[HttpPost]
	[ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
	public async Task<long> Set(InputCloth inputCloth)
	{
		var clotchParameters =
			await _dbContext.ClotchParameterPairs.Where(p =>
				inputCloth.ClotchParametersIds.Contains(p.Id)).ToListAsync();

		var clotch = await _dbContext.Clotches.AddAsync(new Cloth
			{ClotchParameters = clotchParameters, Name = inputCloth.Name, Type = inputCloth.Type});

		await _dbContext.SaveChangesAsync();

		return clotch.Entity.Id;
	}

	/// <summary>
	///     Получить первую попавшуюся информацию о предмете одежды
	/// </summary>
	/// <param name="id">если указано, ищет по идентификатору</param>
	/// <param name="name">если указано, ищет по названию</param>
	/// <param name="type">если указано, ищет по типу</param>
	/// <returns>Результат поиска. Если найдено, то в нем будет информация о предмете одежды</returns>
	[HttpGet]
	[ProducesResponseType(typeof(OutputSearchResult<OutputCloth>), StatusCodes.Status200OK)]
	public async Task<OutputSearchResult<OutputCloth>> Get([FromQuery] long? id = null,
		[FromQuery] string? name = null,
		[FromQuery] string? type = null)
	{
		Cloth? clotch = null;

		var withInclude = _dbContext.Clotches.Include(c => c.ClotchParameters);

		if (id is not null)
			clotch = await withInclude.FirstOrDefaultAsync(c => c.Id == id);
		else if (name is not null)
			clotch = await withInclude.FirstOrDefaultAsync(c => c.Name == name);
		else if (type is not null) clotch = await withInclude.FirstOrDefaultAsync(c => c.Type == type);

		if (clotch is null) return new OutputSearchResult<OutputCloth>(null);

		/*var output = new OutputCloth()
		{
			Id = clotch.Id, Name = clotch.Name, Type = clotch.Type,
			ClotchParameters = clotch.ClotchParameters.Select(p => new OutputClothParameterPair()
			{
				Id = p.Id, Key = p.Key, Value = p.Value
			}).ToList()
		};*/

		return new OutputSearchResult<OutputCloth>(_mapper.Map<OutputCloth>(clotch));
	}
}