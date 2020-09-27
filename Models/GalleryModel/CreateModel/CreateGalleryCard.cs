using System.Collections.Generic;

namespace MyAppAPI.Models.GalleryModel.CreateModel
{
    public class CreateGalleryCard
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public string Descritpion { get; set; }
    }
}