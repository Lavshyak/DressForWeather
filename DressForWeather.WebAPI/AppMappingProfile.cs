using AutoMapper;
using DressForWeather.SharedModels.Inputs;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;

namespace DressForWeather.WebAPI;

public class AppMappingProfile : Profile
{
	public AppMappingProfile()
	{
		CreateMap<Clotch, OutputClotch>();
		CreateMap<ClotchParameterPair, OutputClotchParameterPair>();
		CreateMap<DressReport, OutputDressReport>().ForMember(
			dest => dest.UserReporterId,
			opt => opt.MapFrom(
				src => src.UserReporter.Id
				)
			);
		CreateMap<WeatherState, OutputWeatherState>();

		/*CreateMap<InputClotch, Clotch>();
		CreateMap<InputClotchParameterPair, ClotchParameterPair>();
		CreateMap<InputDressReport, DressReport>();
		CreateMap<InputWeatherState, WeatherState>();*/
	}
}