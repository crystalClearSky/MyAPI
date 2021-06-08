using System.Collections.Generic;
using Entities;
using MyAppAPI.Entities;

namespace Models.ReMap
{
    public class PageDto
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public List<UniqueVisitEntity> UniqueVisits { get; set; }
        private int uniqueVisitsCount;
        public int UniqueVisitsCount
        {
            get { return UniqueVisits.Count; }
        }
        
        public List<CardEntity> TotalPosts { get; set; }

        private int totalPostsCount;
        public int TotalPostsCount
        {
            get { return TotalPosts.Count; }
        }
        
    }
}