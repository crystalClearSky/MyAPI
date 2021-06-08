using System;

namespace Entities.CreateCardEntity
{
    public class CreateImageForRemap
    {
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string QuickDescription { get; set; }
        public int CardEntityId { get; set; }
        public DateTime UploadedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}