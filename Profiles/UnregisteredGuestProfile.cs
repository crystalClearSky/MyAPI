using AutoMapper;

namespace Profiles
{
    public class UnregisteredGuestProfile: Profile
    {
        public UnregisteredGuestProfile()
        {
            CreateMap<Entities.UnregisteredGuestEnitity, Entities.CreateCardEntity.CreateUnregisteredGuestForRemap>().ReverseMap();
            CreateMap<Entities.UnregisteredGuestEnitity, Entities.Simple.UnregisteredGuestSimple>().ReverseMap();
            CreateMap<Entities.CreateCardEntity.CreateUnregisteredGuestForRemap, Entities.Simple.UnregisteredGuestBasic>().ReverseMap();
        }
    }
}