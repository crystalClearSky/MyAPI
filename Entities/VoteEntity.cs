using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace MyAppAPI.Entities
{
    public class VoteEntity
    {
        [Key]
        public int Id { get; set; }
        //public bool UpVote { get; set; }
        [ForeignKey("VoteById")]
        public virtual AvatarEntity AvatarEntity { get; set; }
        public int? VoteById { get; set; }
        [ForeignKey("VoteByGuest")]
        public virtual GuestEntity GuestEntity { get; set; }
        public int? VoteByGuest { get; set; }
        [ForeignKey("CardId")]
        public CardEntity CardEntity { get; set; }
        public int CardId { get; set; }
    }
}