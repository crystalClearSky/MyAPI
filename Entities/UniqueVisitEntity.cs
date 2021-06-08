using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAppAPI.Entities;

namespace Entities
{
    public class UniqueVisitEntity
    {
        [Key]
        public int Id { get; set; }
        public int PageId { get; set; }
        [ForeignKey("PageId")]
        public PageEntity PageEntity { get; set; }
        public int? AvatarVisited { get; set; }
        [ForeignKey("AvatarVisited")]
        public virtual AvatarEntity AvatarEntity { get; set; }
        public int? GuestVisited { get; set; }
        [ForeignKey("GuestVisited")]
        public virtual GuestEntity GuestEntity { get; set; }
        public int? UnregisteredGuestVisited { get; set; }
        [ForeignKey("UnregisteredGuestVisited")]
        public virtual UnregisteredGuestEnitity UnregisteredGuest { get; set; }
        public int? AnonymousId { get; set; }
        [ForeignKey("AnonymousId")]
        public virtual AnonymousEntity AnonymousView { get; set; }
        public DateTime TimeVisited { get; set; }
    }
}