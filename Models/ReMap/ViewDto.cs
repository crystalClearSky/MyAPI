using System;

namespace Models.ReMap
{
    public class ViewDto
    {
        public int Id { get; set; }
        public int? ViewedByGuestId { get; set; }
        public int? ViewedByAvatarId { get; set; }
        public int? ViewedByUnregisteredGuest { get; set; }
        public int? CardEntityId { get; set; }
        public int NumberOfTimesSeen { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
    }
}