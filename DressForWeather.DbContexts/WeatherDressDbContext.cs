using DressForWeather.Models.EFCoreModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.DbContexts;

// TODO: сделать миграции поновой. https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/providers?tabs=dotnet-core-cli

public class WeatherDressDbContext : IdentityDbContext<User, IdentityRole<long>, long>, IWeatherDressDbContext
{
	/*
	 public virtual DbSet<(User)TUser> Users { get; set; } = default!; //IdentityDbContext-> IdentityDbContext -> IdentityUserContext.Users
	 
	 from IdentityDbContext-> IdentityDbContext:
	 public virtual DbSet<TUserClaim> UserClaims { get; set; } = default!;
	 public virtual DbSet<TUserLogin> UserLogins { get; set; } = default!;
	 public virtual DbSet<TUserToken> UserTokens { get; set; } = default!;
	*/

	public DbSet<Clotch> Clotches { get; } = default!;
	public DbSet<ClotchParameterPair> ClotchesParameterPairs { get; protected set; } = default!;
	public DbSet<ClothesSet> ClothesSets { get;  } = default!;
	public DbSet<ClothType> ClothTypes { get;  } = default!;
	public DbSet<DressReport> DressReports { get;  } = default!;
	
	public DbSet<WeatherState> WeatherStates { get;  } = default!;

	public WeatherDressDbContext(DbContextOptions<WeatherDressDbContext> options) : base(options)
	{
		
	}
}