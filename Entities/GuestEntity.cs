using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyAppAPI.Entities;

namespace Entities
{
    public class GuestEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IPAddress { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public List<UniqueVisitEntity> UniqueVisitEntity { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
        public List<VoteEntity> Votes { get; set; } = new List<VoteEntity>();
        public List<ViewEntity> ViewedContent { get; set; } = new List<ViewEntity>();

    }
}