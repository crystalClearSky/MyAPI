using AutoMapper;

namespace Profiles
{
    public class ViewProfile: Profile
    {
        public ViewProfile()
        {
            CreateMap<Entities.ViewEntity, Entities.CreateCardEntity.CreateViewForRemap>().ReverseMap();
        }
    }
}