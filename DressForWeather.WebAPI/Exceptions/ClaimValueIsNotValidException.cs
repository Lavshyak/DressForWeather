namespace DressForWeather.WebAPI.Exceptions;

public class ClaimValueIsNotValidException : Exception
{
	public ClaimValueIsNotValidException(string? message) : base(message)
	{
	}

	public ClaimValueIsNotValidException()
	{
	}
}