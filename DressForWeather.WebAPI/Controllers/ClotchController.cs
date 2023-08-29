using DressForWeather.SharedModels.Inputs;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
public class ClotchController : ControllerBaseDressForWeather
{
	private readonly MainDbContext _dbContext;

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
	public async Task<long> AddClotch(InputClotch inputClotch)
	{
		var clotchParameters =
			await _dbContext.ClotchesParameterPairs.Where(p =>
				inputClotch.ClotchParametersIds.Contains(p.Id)).ToListAsync();

		var clotch = await _dbContext.Clotches.AddAsync(new Clotch
			{ClotchParameters = clotchParameters, Name = inputClotch.Name, Type = inputClotch.Type});
		return clotch.Entity.Id;
	}

	[HttpGet]
	public async Task<Clotch?> GetClotchById(long id)
	{
		var clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Id == id);
		return clotch;
	}

	[HttpGet]
	public async Task<Clotch?> GetClotchByName(string name)
	{
		var clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Name == name);
		return clotch;
	}

	[HttpGet]
	public async Task<Clotch?> GetClotchByType(string type)
	{
		var clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Type == type);
		return clotch;
	}
}