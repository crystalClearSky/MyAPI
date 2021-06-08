using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using MyAppAPI.Entities;
using MyAppAPI.ReMap;

namespace MyAppAPI.Entities.CreateCardEntity
{
    public class CreateGalleryForReMap
    {
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Title { get; set; }
        //[MinLength(10)]
        // public string ImageUrl { get; set; }
        public List<ImageEntity> Images { get; set; } = new List<ImageEntity>();

        [MaxLength(250)]
        public string Description { get; set; }
        public string Content { get; set; }
        [Required]
        public bool IsSuperUser { get; set; }
        public List<TagEntity> Tags { get; set; } = new List<TagEntity>();

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public PostType PostType { get; set; }
    }
}