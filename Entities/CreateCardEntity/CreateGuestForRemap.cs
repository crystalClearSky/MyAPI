using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyAppAPI.Entities;

namespace Entities.CreateCardEntity
{
    public class CreateGuestForRemap
    {
        [Required]
        public string IPAddress { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
    }
}