using AutoMapper;
using RegisterUser.BusinessLayer.Models.LocationData.ResponseModels;
using RegisterUser.BusinessLayer.Models.Users.ResponseModels;
using RegisterUser.DAL.Models;

namespace RegisterUser.API.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserDetail, UserResponseModel>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Province.Country.Name))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province.Name));

            CreateMap<Country, LocationDataModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CountryId));

            CreateMap<Province, LocationDataModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProvinceId));
        }
    }
}
