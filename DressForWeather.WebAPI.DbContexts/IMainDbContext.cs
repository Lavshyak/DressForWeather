using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.DbContexts;

public interface IMainDbContext
{
	public const string DbContextName = "Main";

	public DbSet<Cloth> Clotches { get; }
	public DbSet<ClothParameterPair> ClotchParameterPairs { get; }
	public DbSet<DressReport> DressReports { get; }
	public DbSet<User> Users { get; }
	public DbSet<WeatherState> WeatherStates { get; }
}