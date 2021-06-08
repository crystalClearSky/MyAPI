using System.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyAppAPI.Entities;

namespace Entities
{
    public class ViewEntity
    {
        public int Id { get; set; }
        public int? ViewedByGuestId { get; set; }
        [ForeignKey("ViewedByGuestId")]
        public virtual GuestEntity GuestView { get; set; }
        public int? ViewedByAvatarId { get; set; }
        [ForeignKey("ViewedByAvatarId")]
        public virtual AvatarEntity AvatarView { get; set; }
        public int? ViewedByUnregisteredGuest { get; set; }
        [ForeignKey("ViewedByUnregisteredGuest")]
        public virtual UnregisteredGuestEnitity UnregisteredGuestView { get; set; }
        public int? AnonymousId { get; set; }
        [ForeignKey("AnonymousId")]
        public virtual AnonymousEntity AnonymousView { get; set; }
        // --- Card views --- >
        public int? CardEntityId { get; set; }
        
        public int NumberOfTimesSeen { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
    }
}