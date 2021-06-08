using System;
using System.Collections.Generic;
using Entities;
using MyAppAPI.Entities;

namespace Models.ReMap
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
        public List<VoteEntity> Votes { get; set; } = new List<VoteEntity>();
        private int numberOfVotes;
        public int NumberOfVotes
        {
            get { return Votes.Count; }
        }
        public List<ViewEntity> ContentViewed { get; set; } = new List<ViewEntity>();
        private int contentViewCount;
        public int ContentViewCount
        {
            get { return ContentViewed.Count; }
        }
    }
}