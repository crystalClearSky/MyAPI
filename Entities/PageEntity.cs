using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyAppAPI.Entities;

namespace Entities
{
    public class PageEntity
    {
        //This is a particular page's ID
        [Key]
        public int Id { get; set; }
        public string PageName { get; set; }
        public List<UniqueVisitEntity> UniqueVisits { get; set; }
        public List<CardEntity> TotalPosts { get; set; }
        // public List<AvatarEntity> Members { get; set; }
        // public List<CommentEntity> TotalComments { get; set; }
    }
}