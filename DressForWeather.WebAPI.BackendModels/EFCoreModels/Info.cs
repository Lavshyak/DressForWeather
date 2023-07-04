namespace DressForWeather.WebAPI.BackendModels.EFCoreModels;

/// <summary>
/// не использовать
/// </summary>
[Obsolete("Do not use.")]
public class Info
{
	public long Id { get; set; }
	public DateTime DateTime { get; set; }
	public float Temperature { get; set; }
	/*/// <summary>
	/// тут будет точное направление на будущее (радианы), где 0 - восточный, а Math.Pi/2 - северный
	/// </summary>
	public Half WindDirectionPi { get; set; }
	
	[NotMapped] public WindDirection WindDirectionSimple
	{
		get
		{
			throw new NotImplementedException();
			if(WindDirectionPi > (Half)(float.Pi*1/3) && WindDirectionPi < (Half) (float.Pi*3/4))
				return WindDirection.North;
		}
		set
		{
			throw new NotImplementedException();
			if(value == WindDirection.North)
				WindDirectionPi = (Half) (float.Pi / 2);
		}
	}*/
}