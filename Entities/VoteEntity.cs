using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Entities
{
    public class VoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool UpVote { get; set; } = false;
        [ForeignKey("VoteById")]
        public AvatarEntity AvatarEntity { get; set; }
        public int VoteById { get; set; }
        [ForeignKey("CardId")]
        public CardEntity CardEntity { get; set; }
        public int CardId { get; set; }
    }
}