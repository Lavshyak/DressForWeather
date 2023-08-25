namespace DressForWeather.WebAPI.Exceptions;

public class ClaimValueIsNotWalidException : Exception
{
	public ClaimValueIsNotWalidException(string? message) : base(message)
	{
		
	}
	
	public ClaimValueIsNotWalidException()
	{
		
	}
}