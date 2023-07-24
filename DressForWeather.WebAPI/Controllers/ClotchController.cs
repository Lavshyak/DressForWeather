
using Microsoft.AspNetCore.Mvc;
using DressForWeather.SharedModels;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClotchController : Controller
{
    private readonly ILogger<ClotchController> _logger;
    private readonly MainDbContext _dbContext;

    public ClotchController(ILogger<ClotchController> logger, MainDbContext db) 
    {
        _logger = logger;
        _dbContext = db;
    }

    [HttpGet(nameof(GetClotchById))]
    public async Task<Clotch?> GetClotchById(long id) 
    {
        var clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Id == id);
        return clotch;
    }
    [HttpGet(nameof(GetClotchByNama))]
    public async Task<Clotch?> GetClotchByNama(string name) 
    {
        var clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c=>c.Name == name);
        return clotch;
    }
    [HttpGet(nameof(GetClotchByType))]
    public async Task<Clotch?> GetClotchByType(string type) 
    {
        var clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Type == type);
        return clotch;
    }
}
