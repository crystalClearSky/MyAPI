using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class EnableOptionsEntity
    {
        [Key]
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsUnregistered { get; set; }
        public bool EnableGuest { get; set; }
        public bool IsMember { get; set; }
        public bool IsLiving { get; set; }
        public int VisitCount { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
    }
}