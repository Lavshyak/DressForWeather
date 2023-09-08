using System.Security.Claims;
using DressForWeather.WebAPI.Exceptions;

namespace DressForWeather.WebAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
	/// <summary>
	/// Получить идентификатор пользователя, представленного обьектом ClaimsPrincipal
	/// </summary>
	/// <param name="claimsPrincipal">User</param>
	/// <returns>Id</returns>
	/// <exception cref="ClaimValueIsNotValidException">Проблемы с Id пользователя</exception>
	public static long GetId(this ClaimsPrincipal claimsPrincipal)
	{
		if (!long.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
			throw new ClaimValueIsNotValidException(
				$"NameIdentifier is not valid (parse to {userId.GetType().Name} failed)");

		return userId;
	}
}