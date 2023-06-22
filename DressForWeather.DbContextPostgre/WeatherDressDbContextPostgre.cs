using DressForWeather.DbContextTemplates;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.DbContextPostgre;

public class WeatherDressDbContextPostgre : WeatherDressDbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DressForWeather;Username=postgres;Password=1234");
	}
}