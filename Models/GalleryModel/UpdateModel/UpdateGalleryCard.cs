using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAppAPI.Models.GalleryModel.UpdateModel
{
    public class UpdateGalleryCard
    {
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        [MaxLength(250)]
        public string Descritpion { get; set; }
    }
}