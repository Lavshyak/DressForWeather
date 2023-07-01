using BlazorWithIdentity.Shared;
using DressForWeather.Models.EFCoreModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DressForWeather.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizeController : ControllerBase
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	
	public AuthorizeController(UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	[HttpPost(nameof(Login))]
	public async Task<IActionResult> Login(LoginParameters parameters)
	{
		var user = await _userManager.FindByNameAsync(parameters.UserName);
		if (user == null) return BadRequest("User does not exist");
		var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
		if (!singInResult.Succeeded) return BadRequest("Invalid password");

		await _signInManager.SignInAsync(user, parameters.RememberMe);

		return Ok();
	}


	[HttpPost(nameof(Register))]
	public async Task<IActionResult> Register(RegisterParameters parameters)
	{
		var user = new User
		{
			UserName = parameters.UserName
		};
		
		//line below throws exception: relation "AspNetUsers" does not exist.
		// TODO: исправить ошибку. в интернете советуют миграции добавить особым образом.
		var result = await _userManager.CreateAsync(user, parameters.Password); 
		if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

		return await Login(new LoginParameters
		{
			UserName = parameters.UserName,
			Password = parameters.Password
		});
	}

	[Authorize]
	[HttpPost(nameof(Logout))]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return Ok();
	}

	[HttpGet(nameof(UserInfo))]
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