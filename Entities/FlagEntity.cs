using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyAppAPI.Entities
{
    public class FlagEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        //public bool IsFlaged { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string ReasonText { get; set; }
        [ForeignKey("AvatarId")]
        public List<AvatarEntity> AvatarEntity { get; set; }
        public int AvatarId { get; set; }
        // [ForeignKey("CardId")]
        // public CardEntity CardEntity { get; set; }
        public int CommentEntityId { get; set; }
    }
}