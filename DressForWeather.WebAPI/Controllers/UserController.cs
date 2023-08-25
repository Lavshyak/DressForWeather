using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
public class UserController : ControllerBaseDressForWeather
{
	private SignInManager<User> _signInManager;
	private UserManager<User> _userManager;

	public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	/*[HttpPost(nameof(ChangeCurrentName))]
	//[ValidateAntiForgeryToken]
	public async Task<IActionResult> ChangeCurrentName(User user,UserInfo info,string name)
	{
	    if (info.IsAuthenticated == true && user != null)
	    {
	        user.UserName = name;
	    }
	    return Ok();
	}*/

	/*[HttpPost(nameof(ChangeCurrentPassword))]
	//[ValidateAntiForgeryToken]
	public async Task<IActionResult> ChangeCurrentPassword(User user,UserInfo info,string password)
	{
	    if(info.IsAuthenticated == true && user != null)
	    {
	        //user.password = password
	    }
	    return Ok();
	}*/
}