using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyAppAPI.Entities
{
    public class TagEntity
    {
        [Key]
        public int Id { get; set; }
        public string TagItem { get; set; }
        [ForeignKey("CardId")]
        public CardEntity CardEntity { get; set; }
        public int CardId { get; set; }
        [ForeignKey("ByAvatarId")]
        public AvatarEntity AvatarEntity { get; set; }
        public int ByAvatarId { get; set; }
        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; }
        public int? UserId { get; set; }

    }
}