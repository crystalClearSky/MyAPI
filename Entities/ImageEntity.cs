using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using MyAppAPI.Entities;
using System.ComponentModel;
using System.Collections.Generic;

namespace Entities
{
    public class ImageEntity
    {
        [Key]
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