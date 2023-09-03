#if DEBUG

using System.Security.Claims;
using DressForWeather.SharedModels;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DressForWeather.WebAPI.Controllers;

public class DebugController : ControllerBaseWithRouteToAction
{
	private readonly SignInManager<User> _signInManager;
	private readonly UserManager<User> _userManager;

	public DebugController(SignInManager<User> signInManager, UserManager<User> userManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
	}

	[HttpGet]
	public string Foo()
	{
		return "sss";
	}

	[HttpGet]
	public string GetMyId()
	{
		return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
	}


	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> DebugLogiAndGetInfo(LoginParameters parameters)
	{
		var user = await _userManager.FindByNameAsync(parameters.UserName);
		if (user == null) return BadRequest("User does not exist");
		var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
		if (!singInResult.Succeeded) return BadRequest("Invalid password");

		await _signInManager.SignInAsync(user, parameters.RememberMe);
		return new JsonResult(BuildUserInfo());
	}

	[AllowAnonymous]
	[HttpGet]
	public UserInfo UserInfo()
	{
		//var user = await _userManager.GetUserAsync(HttpContext.User);
		return BuildUserInfo();
	}


	private UserInfo BuildUserInfo()
	{
		return new UserInfo
		{
			IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
			UserName = User.Identity?.Name ?? "unknown",
			ExposedClaims = User.Claims
				//Optionally: filter the claims you want to expose to the client
				//.Where(c => c.Type == "test-claim")
				.ToDictionary(c => c.Type, c => c.Value)
		};
	}
}

#endif