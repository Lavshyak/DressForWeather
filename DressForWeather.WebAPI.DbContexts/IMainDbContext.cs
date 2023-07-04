using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.DbContexts;

public interface IMainDbContext
{
	public const string DbContextName = "Main";
	
	public DbSet<Clotch> Clotches { get; }
	public DbSet<ClotchParameterPair> ClotchesParameterPairs { get; }
	public DbSet<ClothesSet> ClothesSets { get; }
	public DbSet<ClothType> ClothTypes { get; }
	public DbSet<DressReport> DressReports { get; }
	public DbSet<User> Users { get; }
	public DbSet<WeatherState> WeatherStates { get; }
}