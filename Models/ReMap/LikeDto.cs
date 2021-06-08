namespace MyAppAPI.ReMap
{
    public class LikeDto
    {
        public int Id { get; set; }
        public bool HasLiked { get; set; } = false;
        public int LikedById { get; set; }
        public int CommentId { get; set; }
    }
}