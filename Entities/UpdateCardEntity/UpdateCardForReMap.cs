using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using MyAppAPI.ReMap;

namespace MyAppAPI.Entities.UpdateCardEntity
{
    public class UpdateCardForReMap
    {
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Title { get; set; }

        // public string ImageUrl { get; set; }
        public List<ImageEntity> Images { get; set; } = new List<ImageEntity>();

        public List<TagEntity> Tags { get; set; } = new List<TagEntity>();
        [MaxLength(250)]
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsSuperUser { get; set; }
        public PostType PostType { get; set; }
    }
}