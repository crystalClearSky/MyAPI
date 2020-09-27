using System;
using System.Collections.Generic;
using MyAppAPI.AppRepository;
using MyAppAPI.Services;

namespace MyAppAPI.Models.GalleryModel
{
    public class Gallery
    {
        public int Id { get; set; }
        // Title is equal to currently selected tag
        public string Title { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>() { new Tag() { TagItem = "3dWorks" } };
        public List<GalleryCard> GalleryCards { get; set; }

        public IEnumerable<GalleryCard> GetGalleryByTag()
        {
            IGalleryData galleryDb = new GalleryData();
            var galleryCards = galleryDb.GetGalleryCardsByTags(this.Tags); // CHANGE!!
            foreach (var item in this.Tags)
            {
                Console.WriteLine((string)item.TagItem);
            }
            if (galleryCards == null)
            {
                galleryCards = new List<GalleryCard>();
            }
            return galleryCards;
        }
    }


}