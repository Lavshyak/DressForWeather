using DressForWeather.WebAPI;

//для дебага, когда приложение запущено не из IDE
#if LAUNCH_DEBUGGER
System.Diagnostics.Debugger.Launch();
#endif

var builder = WebApplication.CreateBuilder(args);
//первичная настройка сервера
builder.ConfigureServices();

var app = builder.Build();
//вторичная настройка сервера
app.ConfigureApp();

//запустить цикл прослушки портов и ответов на запросы. выйдет из этого метода тогда, когда сервер будет остановлен.
app.Run();

// For Tests
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program
{
}