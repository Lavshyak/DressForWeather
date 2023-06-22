using DressForWeather.Models.EFCoreModels;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.DbContextTemplates;

// TODO: сделать миграции поновой. https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/providers?tabs=dotnet-core-cli

public abstract class WeatherDressDbContext : DbContext, IWeatherDressDbContext
{
	public DbSet<Clotch> Clotches { get; protected set; } = default!;
	public DbSet<ClotchParameterPair> ClotchesParameterPairs { get; protected set; } = default!;
	public DbSet<ClothesSet> ClothesSets { get; protected set; } = default!;
	public DbSet<ClothType> ClothTypes { get; protected set; } = default!;
	public DbSet<DressReport> DressReports { get; protected set; } = default!;
	public DbSet<User> Users { get; protected set; } = default!;
	public DbSet<WeatherState> WeatherStates { get; protected set; } = default!;
}