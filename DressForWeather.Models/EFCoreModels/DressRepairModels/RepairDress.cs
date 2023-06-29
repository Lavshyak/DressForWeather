using System;
using DressForWeather.Models.EFCoreModels;
using DressForWeather.DbContextTemplates;

namespace DressForWeather.Models.EFCoreModels.DressRepairModels.

public class RepairDress:IDressRepos
{
	private WeatherDressDbContext DbContext { get; set; }
    public RepairDress(WeatherDressDbContext db)
    {
        DbContext = db;
    }
    public async List<Task<DressReport>> GetAllDressReport() 
    {
        var lst = await DbContext.Set<DressModel>.ToList();
        return lst ;
    }
    public async Task<DressReport> Create(DressReport model) 
	{
        DbContext.Set<DressReport>().Add(model);
        await DbContext.SaveChanges();
        return model;
	}
    public async void Delete(DressReport model) 
    {
        var toDelete = await DbContext.Set<DressReport>().FirstOrDefaultAsync(c => c.id == model.Id);
        DbContext.Set<DressReport>().Remove(toDelete);
        DbContext.SaveChanges();
        
    }
    public async Task<DressReport> Update(DressReport model) 
    {
        var toUpdate = await DbContext.Set<DressReport>().FirstOrDefaultAsync(c=> c.id == model.Id);
        if(toUpdate != null)
        {
            toUpdate = model;
        }
        DbContext.Update(toUpdate);
        DbContext.SaveChanges();
        return toUpdate;
    }
    
}
