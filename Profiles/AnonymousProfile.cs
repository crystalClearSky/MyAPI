using AutoMapper;

namespace Profiles
{
    public class AnonymousProfile: Profile
    {
        public AnonymousProfile()
        {
            CreateMap<Entities.AnonymousEntity, Entities.CreateCardEntity.CreateAnonymousforRemap>().ReverseMap();
        }
    }
}