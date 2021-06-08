using AutoMapper;

namespace MyAppAPI.Profiles
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<Entities.LikeEntity, Entities.Simple.AddLikeDto>().ReverseMap();
        }
    }
}