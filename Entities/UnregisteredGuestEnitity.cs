using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class UnregisteredGuestEnitity
    {
        // Transfer data to GuestEntity before becoming an Avatar
        [Key]
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public bool EnableGuest { get; set; }
        public UniqueVisitEntity UniqueVisitEntity { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
        public List<ViewEntity> ViewedContent { get; set; } = new List<ViewEntity>();


    }
}