using System;

namespace Entities.Simple
{
    public class GuestSimple
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }

    }
}