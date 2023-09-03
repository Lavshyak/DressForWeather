using DressForWeather.SharedModels;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
[ProducesErrorResponseType(typeof(string))]
[ProducesResponseType(StatusCodes.Status200OK)]
public class AuthorizeController : ControllerBaseWithRouteToAction
{
	private readonly SignInManager<User> _signInManager;
	private readonly UserManager<User> _userManager;

	public AuthorizeController(UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	[AllowAnonymous]
	[HttpPost]
	[ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
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
	[ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Register(RegisterParameters parameters)
	{
		var user = new User
		{
			UserName = parameters.UserName
		};

		var result = await _userManager.CreateAsync(user, parameters.Password);
		if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

		//добавляет юзеру роль newReg
		await _userManager.AddToRoleAsync(user, "User");

		return await Login(new LoginParameters
		{
			UserName = parameters.UserName,
			Password = parameters.Password
		});
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return Ok();
	}
}