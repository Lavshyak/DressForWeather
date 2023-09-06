using AutoMapper;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;

namespace DressForWeather.WebAPI;

public class AppMappingProfile : Profile
{
	public AppMappingProfile()
	{
		CreateMap<Clotch, OutputClotch>().ForMember(
			dest => dest.ClotchParametersIds,
			opt => opt.MapFrom(
				src => src.ClotchParameters.Select(p => p.Id)
			)
		);
		CreateMap<ClotchParameterPair, OutputClotchParameterPair>();

		CreateMap<DressReport, OutputDressReport>().ForMember(
			dest => dest.UserReporterId,
			opt => opt.MapFrom(
				src => src.UserReporter.Id
			)
		).ForMember(
			dest => dest.ClothIds,
			opt => opt.MapFrom(
				src => src.Clothes.Select(p => p.Id)
			)
		);

		CreateMap<WeatherState, OutputWeatherState>();

		/*CreateMap<InputClotch, Clotch>();
		CreateMap<InputClotchParameterPair, ClotchParameterPair>();
		CreateMap<InputDressReport, DressReport>();
		CreateMap<InputWeatherState, WeatherState>();*/
	}
}