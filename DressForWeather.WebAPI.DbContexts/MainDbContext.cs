using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.DbContexts;

public class MainDbContext : IdentityDbContext<User, IdentityRole<long>, long>, IMainDbContext
{
	public const string ContextName = "Main";

	public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
	{
	}

	/*
	 public virtual DbSet<(User)TUser> Users { get; set; } = default!; //IdentityDbContext-> IdentityDbContext -> IdentityUserContext.Users

	 from IdentityDbContext-> IdentityDbContext:
	 public virtual DbSet<TUserClaim> UserClaims { get; set; } = default!;
	 public virtual DbSet<TUserLogin> UserLogins { get; set; } = default!;
	 public virtual DbSet<TUserToken> UserTokens { get; set; } = default!;
	*/

	public DbSet<Clotch> Clotches { get; protected set; } = default!;
	public DbSet<ClotchParameterPair> ClotchParameterPairs { get; protected set; } = default!;
	public DbSet<DressReport> DressReports { get; protected set; } = default!;

	public DbSet<WeatherState> WeatherStates { get; protected set; } = default!;
}