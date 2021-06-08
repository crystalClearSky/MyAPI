using System.Net.Security;
using System.Dynamic;
using AutoMapper;

namespace MyAppAPI.Profiles
{
    public class AvatarProfile : Profile
    {
        public AvatarProfile()
        {
            CreateMap<Entities.AvatarEntity, ReMap.AvatarDto>().ReverseMap();
            CreateMap<Entities.AvatarEntity, Entities.CreateCardEntity.CreateAvatarForRemap>().ReverseMap();
            CreateMap<Entities.AvatarEntity, Entities.Simple.AvatarSimple>().ReverseMap();
            
        }
    }
}