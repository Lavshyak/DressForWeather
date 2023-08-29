using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI;

public partial class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	private IConfiguration Configuration { get; }

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		// Add services to the container.

		services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddDbContext<MainDbContext>(
			options =>
			{
				var dbProvider =
					GetDbProviderNameFor(Configuration, MainDbContext.ContextName, DbProviders.Postgre);
				_ = dbProvider
					switch
					{
						//сделано: dotnet ef migrations add InitialCreate --project ../DressForWeather.WebAPI.PostgreMigrations -- --provider Postgre
						//потом: dotnet ef database update
						DbProviders.Postgre => options.UseNpgsql(
							GetConnectionStringForDbContext(Configuration, dbProvider,
								MainDbContext.ContextName),
							x => x.MigrationsAssembly(GetMigrationsAssemblyNameFor(dbProvider))),

						//если перейдем на другую базу данных, то нужно сделать действия, аналогичные строке выше

						_ => throw new Exception($"Unsupported provider: {dbProvider}")
					};
			});

		services.AddScoped<MainDbContext>();

		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie();

		services.AddAuthorization();
		AddManualAuthorization(services);
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		// Configure the HTTP request pipeline.
		if (env.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

		SynchronizeIdentityRoles(app.ApplicationServices).GetAwaiter().GetResult();
	}
	
	
}