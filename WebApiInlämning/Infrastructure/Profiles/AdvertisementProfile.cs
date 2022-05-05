using AutoMapper;
using WebApiInlämning.Data;
using WebApiInlämning.DTO;

namespace WebApiInlämning.Infrastructure.Profiles
{
    public class AdvertisementProfile : Profile
    {
        public AdvertisementProfile()
        {
            CreateMap<Advertisement, AdvertisementDTO>().ReverseMap();
            CreateMap<Advertisement, AdvertisementsDTO>().ReverseMap();
            CreateMap<Advertisement, CreateAdvertisementDTO>().ReverseMap();
            CreateMap<Advertisement, UpdateAdvertisementDTO>().ReverseMap();

        }
    }
}
