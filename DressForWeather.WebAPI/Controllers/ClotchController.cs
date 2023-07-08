
using Microsoft.AspNetCore.Mvc;
using DressForWeather.SharedModels;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClotchController : Controller
{
    private readonly ILogger<ClotchController> _logger;
    private readonly WeatherDressDbContext _dbContext;

    public ClotchController(ILogger<ClotchController> logger, WeatherDressDbContext db) 
    {
        _logger = logger;
        _dbContext = db;
    }
    [HttpPost]
    public async Task<Clotch> CreateClotch
    [HttpGet]
    public async Task<Clotch?> GetClotchById(long id) 
    {
        var clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Id == id);
        return clotch;
    }
    [HttpGet]
    public async Task<Clotch?> GetClotchByNama(string name) 
    {
        var clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c=>c.Name == name);
        return clotch;
    }
    [HttpGet]
    public async Task<Clotch?> GetClotchByType(string type) 
    {
        var clotch = await _dbContext.Clotches.FirstOrDefaultAsync(c => c.Type == type);
        return clotch;
    }
}
