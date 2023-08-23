using System.Security.Claims;
using System.Security.Principal;
using DressForWeather.SharedModels;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
public class AuthorizeController : ControllerBaseDressForWeather
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	
	public AuthorizeController(UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> Login(LoginParameters parameters)
	{
		var user = await _userManager.FindByNameAsync(parameters.UserName);
		if (user == null) return BadRequest("User does not exist");
		var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
		if (!singInResult.Succeeded) return BadRequest("Invalid password");

		await _signInManager.SignInAsync(user, parameters.RememberMe);

		return Ok();
	}


	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> Register(RegisterParameters parameters)
	{
		var user = new User
		{
			UserName = parameters.UserName
		};
		
		var result = await _userManager.CreateAsync(user, parameters.Password); 
		if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

		//добавляет юзеру роль newReg
		await _userManager.AddToRoleAsync(user, "newReg");
		
		return await Login(new LoginParameters
		{
			UserName = parameters.UserName,
			Password = parameters.Password
		});
	}
	
	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return Ok();
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
	
	#if DEBUG

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
	#endif
}