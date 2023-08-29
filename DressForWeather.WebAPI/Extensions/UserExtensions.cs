using System.Security.Claims;
using DressForWeather.WebAPI.Exceptions;

namespace DressForWeather.WebAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
	public static long GetId(this ClaimsPrincipal claimsPrincipal)
	{
		if (!long.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
			throw new ClaimValueIsNotValidException(
				$"NameIdentifier is not valid (parse to {userId.GetType().Name} failed)");

		return userId;
	}
}