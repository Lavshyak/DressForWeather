using Microsoft.AspNetCore.Mvc;

namespace DressForWeather.WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public abstract class ControllerBaseWithRouteToAction : ControllerBase
{
}