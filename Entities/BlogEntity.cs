using System;
using System.Collections.Generic;

namespace Entities
{
    public class BlogEntity
    {
        public int Id { get; set; }
        public int AvatarEntityId { get; set; }
        public bool UserEntityId { get; set; }
        public string Content { get; set; }
        public List<LinkEntity> Links { get; set; } = new List<LinkEntity>();
        public DateTime PostedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}