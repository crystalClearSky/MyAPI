using System.ComponentModel.DataAnnotations;
namespace MyAppAPI.ReMap
{
    public class FlagDto
    {
        public int id { get; set; }
        //public bool IsFlaged { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(150)]
        public string ReasonText { get; set; }
        public int AvatarId { get; set; }
        public int CommentEntityId { get; set; }
    }
}