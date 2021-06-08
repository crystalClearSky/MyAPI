using System;
using System.ComponentModel;

namespace Models.ReMap
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string QuickDescription { get; set; }

        public int CardEntityId { get; set; }
        public DateTime UploadedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Catergory Catergory { get; set; }
    }

    public enum Catergory
    {
        [Description("Image")]
        Image,
        [Description("YouTube Video")]
        YouTubeVideo,
        [Description("Vimeo Video")]
        VimeoVideo,
        [Description("Web Video")]
        WebVideo,
        [Description("Blog")]
        Blog
    }
}