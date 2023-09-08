using Microsoft.AspNetCore.Mvc;

namespace DressForWeather.WebAPI.Controllers;

/// <summary>
/// Шаблон для контроллера
/// </summary>
[ApiController]
[Route("[controller]")]
public abstract class ControllerBaseWithRouteToController : ControllerBase
{
}