using System.Collections.Generic;
using MyAppAPI.ReMap;

namespace MyAppAPI.ReMap
{
    public class UserDto
    {
        public string Name { get; set; }
        public string AvatarImgUrl { get; set; }
        public List<CardDto> Cards { get; set; } = new List<CardDto>();
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public List<ReplyDto> Replies { get; set; } = new List<ReplyDto>();
        public List<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}