using AutoMapper;

namespace Profiles
{
    public class EnableOptionsProfile: Profile
    {
        public EnableOptionsProfile()
        {
            CreateMap<Entities.EnableOptionsEntity, Entities.CreateCardEntity.CreateEnableOptionsforRemap>().ReverseMap();
        }
    }
}