using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyAppAPI.Entities;

namespace Entities
{
    public class LinkEntity
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string LinkIndex { get; set; }
        public int CommentEntityId { get; set; }
        public LinkType LinkType { get; set; }
        public DateTime AddedOn { get; set; }
    }
    public enum LinkType {
        Web,
        Image,
        Video,
        Gif
    }
}