using System;

namespace Entities.CreateCardEntity
{
    public class CreateEnableOptionsforRemap
    {
        public string IpAddress { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsUnregistered { get; set; }
        public bool EnableGuest { get; set; }
        public bool IsMember { get; set; }
        public int VisitCount { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
    }
}