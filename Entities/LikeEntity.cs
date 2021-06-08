using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Entities
{
    public class LikeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool HasLiked { get; set; } = false;
        [ForeignKey("LikedById")]
        public AvatarEntity AvatarEntity { get; set; }
        public int LikedById { get; set; }
        [ForeignKey("CommentId")]
        public CommentEntity CommentEntity { get; set; }
        public int CommentId { get; set; }

    }
}