using AutoMapper;

namespace MyAppAPI.Profiles
{
    public class FlagProfile : Profile
    {
        public FlagProfile()
        {
            CreateMap<Entities.FlagEntity, ReMap.FlagDto>().ReverseMap();
        }
    }
}