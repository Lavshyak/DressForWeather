using System.ComponentModel.DataAnnotations;

namespace DressForWeather.SharedModels;

public class RegisterParameters
{
	[Required] public required string UserName { get; set; }

	[Required] public required string Password { get; set; }
}