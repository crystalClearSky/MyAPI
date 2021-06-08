using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;
using System.Data.Entity.ModelConfiguration;
using Entities;

namespace MyAppAPI.Entities
{
    public class CardEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        [MinLength(2)]
        public string Title { get; set; }
        // [MinLength(10)]
        // public string ImageUrl { get; set; }
        public List<ImageEntity> Images { get; set; } = new List<ImageEntity>();
        // List of images in a card
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        // [ForeignKey("UserId")]
        // public UserEntity UserEntity { get; set; }
        // public int UserId { get; set; }
        public string Content { get; set; }
        public bool IsSuperUser { get; set; }
        [ForeignKey("IsSuperUser")]
        // PublishedBy id
        public virtual UserEntity UserEntity { get; set; }
        public List<TagEntity> Tags { get; set; } = new List<TagEntity>();

        [MaxLength(350)]
        public string Description { get; set; }
        public int SearchIndexValue { get; set; }
        public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        public List<VoteEntity> UpVotes { get; set; } = new List<VoteEntity>();
        public bool TagIsActive { get; set; } = false;
        public bool ReplyBoxIsActive { get; set; } = false;
        public List<ViewEntity> Views { get; set; } = new List<ViewEntity>();
        public PostType PostType { get; set; }
    }

    public enum PostType {
        Gallery,
        Blog
    } 
    
}