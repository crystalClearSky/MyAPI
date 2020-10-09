using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyAppAPI.Entities;
using MyAppAPI.Models.GalleryModel;

namespace MyAppAPI.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsSuperUser { get; set; }
        public List<GalleryCard> GalleryCards { get; set; }
        public List<CommentEntity> Comments { get; set; }
    }
}