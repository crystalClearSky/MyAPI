using System;

namespace Entities.CreateCardEntity
{
    public class CreateViewForRemap
    {
        public int? ViewedByGuestId { get; set; }
        public int? ViewedByAvatarId { get; set; }
        public int? ViewedByUnregisteredGuest { get; set; }
        public int? AnonymousId { get; set; }
        public int? CardEntityId { get; set; }
        public int NumberOfTimesSeen { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
    }
}