using System;

namespace Entities.CreateCardEntity
{
    public class CreateUnregisteredGuestForRemap
    {
        public string IPAddress { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public bool EnableGuest { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
    }
}