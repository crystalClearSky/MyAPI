using System.Collections.Generic;
using System.Security.AccessControl;
using MyAppAPI.AppRepository;
using MyAppAPI.Services;

namespace MyAppAPI.Models
{
    public class Tag
    {
        // public int Id { get; set; }
        public object TagItem { get; set; }
        public int NumberOfContentWithThisTag
         {
             get
             {
                 return GetNumberOfContentWithThisTag();
             }
         }

        public int GetNumberOfContentWithThisTag()
        {
            int count = 0;
            IGalleryData galleryDb = new GalleryData();
            var tags = new List<Tag>() { new Tag() { TagItem = this.TagItem } };
            var cards = galleryDb.GetGalleryCardsByTags(tags);
            foreach (var item in cards)
            {
                count++;
            }
            return count;
        }
    }
}