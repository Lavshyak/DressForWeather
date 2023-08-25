#if DEBUG

using Microsoft.AspNetCore.Mvc;

namespace DressForWeather.WebAPI.Controllers;

public class TestController : ControllerBaseDressForWeather
{
	[HttpGet]
	public string Foo()
	{
		return "sss";
	}
}

#endif