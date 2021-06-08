using AutoMapper;
using Entities.CreateCardEntity;

namespace MyAppAPI.Profiles
{
    public class GalleryCreateProfile : Profile
    {
        public GalleryCreateProfile()
        {
            CreateMap<Entities.CreateCardEntity.CreateGalleryForReMap, Entities.CardEntity>();
            CreateMap<Entities.UpdateCardEntity.UpdateCardForReMap, Entities.CardEntity>();
            CreateMap<CreateCardWithoutImagesforRemap, Entities.CreateCardEntity.CreateGalleryForReMap>().ReverseMap();
        }
    }
}