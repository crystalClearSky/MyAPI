using AutoMapper;

namespace MyAppAPI.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Entities.CommentEntity, ReMap.CommentDto>();
            CreateMap<Entities.CommentEntity, Entities.CreateCardEntity.CreateCommentForRemap>().ReverseMap();
            CreateMap<Entities.CommentEntity, Entities.UpdateCardEntity.UpdateCommentDto>().ReverseMap();
        }
    }
}