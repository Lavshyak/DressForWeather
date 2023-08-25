using System.Security.Claims;
using DressForWeather.WebAPI.Exceptions;

namespace DressForWeather.WebAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
	public static UserIdType GetId(this ClaimsPrincipal claimsPrincipal)
	{
		if (!UserIdType.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
			throw new ClaimValueIsNotWalidException($"NameIdentifier is not valid (parse to {typeof(UserIdType).BaseType} failed)");

		return userId;
	}
}