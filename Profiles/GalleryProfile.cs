using System.Net.Security;
using AutoMapper;

namespace MyAppAPI.Profiles
{
    public class GalleryProfile : Profile
    {
        public GalleryProfile()
        {
            CreateMap<Entities.CardEntity, ReMap.CardDto>();
            CreateMap<Entities.AvatarEntity, ReMap.AvatarDto>();
            CreateMap<Entities.CommentEntity, ReMap.CommentDto>();
            CreateMap<Entities.FlagEntity, ReMap.FlagDto>();
            CreateMap<Entities.LikeEntity, ReMap.LikeDto>();
            CreateMap<Entities.ReplyEntity, ReMap.ReplyDto>();
            CreateMap<Entities.TagEntity, ReMap.TagDto>();
            CreateMap<Entities.UserEntity, ReMap.UserDto>();
            CreateMap<Entities.VoteEntity, ReMap.UpVoteDto>();

        }
    }
}