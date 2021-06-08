using AutoMapper;

namespace Profiles
{
    public class GuestProfile: Profile
    {
        public GuestProfile()
        {
            CreateMap<Entities.GuestEntity, Entities.CreateCardEntity.CreateGuestForRemap>().ReverseMap();
            CreateMap<Entities.GuestEntity, Entities.Simple.GuestSimple>().ReverseMap();
            CreateMap<Entities.GuestEntity, Models.ReMap.GuestDto>().ReverseMap();
        }
    }
}