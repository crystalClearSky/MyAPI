using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class AnonymousEntity
    {
        [Key]
        public int Id { get; set; }
        public List<UniqueVisitEntity> UniqueVisitEntity { get; set; } = new List<UniqueVisitEntity>();
        public List<ViewEntity> ViewedContent { get; set; } = new List<ViewEntity>();
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
    }
}