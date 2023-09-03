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
public class ClotchController : ControllerBaseWithRouteToController
{
	private readonly IMainDbContext _dbContext;

	public ClotchController(MainDbContext db)
	{
		_dbContext = db;
	}

	/// <summary>
	///     Add clotch to data base
	/// </summary>
	/// <param name="inputClotch"></param>
	/// <returns>Id of added clotch</returns>
	[HttpPost]
	[ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
	public async Task<long> Set(InputClotch inputClotch)
	{
		var clotchParameters =
			await _dbContext.ClotchesParameterPairs.Where(p =>
				inputClotch.ClotchParametersIds.Contains(p.Id)).ToListAsync();

		var clotch = await _dbContext.Clotches.AddAsync(new Clotch
			{ClotchParameters = clotchParameters, Name = inputClotch.Name, Type = inputClotch.Type});
		return clotch.Entity.Id;
	}

	[HttpGet]
	[ProducesResponseType(typeof(OutputSearchResult<Clotch>), StatusCodes.Status200OK)]
	public async Task<OutputSearchResult<Clotch>> Get([FromQuery] long? id = null, [FromQuery] string? name = null,
		[FromQuery] string? type = null)
	{
		Clotch? clotch = null;
		if (id != null)
		{
			clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Id == id);
		}
		else if (name != null)
		{
			clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Name == name);
		}
		else if (type != null)
		{
			clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Type == type);
		}

		return new OutputSearchResult<Clotch>(clotch);
	}

	/*[HttpGet]
	[ProducesResponseType(typeof(OutputSearchResult<Clotch>), StatusCodes.Status200OK)]
	public async Task<OutputSearchResult<Clotch>> Get1([FromQuery] string name)
	{

		return new OutputSearchResult<Clotch>(clotch);
	}

	[HttpGet]
	[ProducesResponseType(typeof(OutputSearchResult<Clotch>), StatusCodes.Status200OK)]
	public async Task<OutputSearchResult<Clotch>> Get2([FromQuery] string type)
	{
		var
		return new OutputSearchResult<Clotch>(clotch);
	}*/
}