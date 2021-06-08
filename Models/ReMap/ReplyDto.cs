using MyAppAPI.ReMap;

namespace MyAppAPI.ReMap
{
    public class ReplyDto
    {
        public int? AvatarId { get; set; }
        public CommentDto Reply { get; set; }
        public int CommentId { get; set; }
        public CommentDto ResponseComment { get; set; }
        public int? ResponseToCommentId { get; set; }
        public bool? IsSuperUser { get; set; }
        public bool HasReplied { get; set; }
    }
}