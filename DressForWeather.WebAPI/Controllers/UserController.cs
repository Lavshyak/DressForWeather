using DressForWeather.SharedModels;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpPost(nameof(ChangeName))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeCurrentName(User user,UserInfo info,string name)
    {
        if (info.IsAuthenticated == true && user != null)
        {
            user.Name = name;
        }
        return Ok();
    }
    [HttpPost(nameof(ChangePassword))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeCurrentPassword(User user,UserInfo info,string password)
    {
        if(info.IsAuthenticated == true && user != null)
        {
            //user.password = password
        }
        return Ok();
    }
}