using DressForWeather.WebAPI;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
app.ConfigureApp();

app.Run();

// For Tests
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program { }