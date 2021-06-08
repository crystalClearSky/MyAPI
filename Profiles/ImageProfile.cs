using AutoMapper;

namespace Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Entities.ImageEntity, Entities.CreateCardEntity.CreateImageForRemap>().ReverseMap();
        }
    }
}