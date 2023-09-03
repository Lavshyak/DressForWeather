using System.Collections.Concurrent;

namespace DressForWeather.SharedModels.Outputs;

public class OutputSearchResult<T>
{
	public OutputSearchResult()
	{
		
	}
	
	public OutputSearchResult(T? entity)
	{
		Entity = entity;
		IsFound = entity != null;
	}
	
	public OutputSearchResult(T? entity, bool isFound)
	{
		Entity = entity;
		IsFound = isFound;
	}
	
	public bool IsFound { get; set; }
	public T? Entity { get; set; }
}