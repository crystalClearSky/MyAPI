namespace MyAppAPI.Models
{
    public class Like
    {
        // The ID corresponses to a GalleryCard ID
        public int Id { get; set; }
        public bool HasLiked { get; set; } = false;
        public int LikedById { get; set; }
    }
}