using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DressForWeather.Models.EFCoreModels;

public class ClothType
{
	public int Id { get; set; }
	
	/// <summary>
	/// это должно быть тем, что можно найти, используя динамичный поиск.
	/// например, пользователь вводит "куртка", и находится этот обьект.
	/// сущности будут ссылаться на этот тип.
	/// </summary>
	// TODO: пользователь может ввести "jacket". должен найтись тот же обьект, который нашелся бы как "куртка".
	// TODO: надо подумать, как базу данных сделать единой для хотя бы двух языков, не дублируя такие свойства.
	public required string Name { get; set; }
}