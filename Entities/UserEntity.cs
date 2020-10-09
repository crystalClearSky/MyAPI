using System.Collections.Generic;

namespace MyAppAPI.Entities
{
    public class UserEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsSuperUser { get; set; }
        public List<CardEntity> Cards { get; set; } = new List<CardEntity>();
        public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        public List<ReplyEntity> Replies { get; set; }
        public List<TagEntity> Tags { get; set; }
    }
}