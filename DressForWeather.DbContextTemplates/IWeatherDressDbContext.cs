using DressForWeather.Models.EFCoreModels;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.DbContextTemplates;

public interface IWeatherDressDbContext
{
	public DbSet<Clotch> Clotches { get; }
	public DbSet<ClotchParameterPair> ClotchesParameterPairs { get; }
	public DbSet<ClothesSet> ClothesSets { get; }
	public DbSet<ClothType> ClothTypes { get; }
	public DbSet<DressReport> DressReports { get; }
	public DbSet<User> Users { get; }
	public DbSet<WeatherState> WeatherStates { get; }
}