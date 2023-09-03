using Microsoft.AspNetCore.Mvc;

namespace DressForWeather.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ControllerBaseWithRouteToController : ControllerBase
{
}