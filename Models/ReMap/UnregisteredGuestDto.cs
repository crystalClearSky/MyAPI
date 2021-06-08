using System;
using System.Collections.Generic;
using Entities;

namespace Models.ReMap
{
    public class UnregisteredGuestDto
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
        public List<ViewEntity> ContentViewed { get; set; } = new List<ViewEntity>();
        private int contentViewCount;
        public int ContentViewCount
        {
            get { return ContentViewed.Count; }
        }
    }
}