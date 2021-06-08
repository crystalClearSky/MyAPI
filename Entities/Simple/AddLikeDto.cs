namespace MyAppAPI.Entities.Simple
{
    public class AddLikeDto
    {
        
        public bool HasLiked { get; set; } = false;
        public int LikedById { get; set; }
        public int CommentId { get; set; }
    }
}