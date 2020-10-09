using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;

namespace MyAppAPI.Entities
{
    public class CardEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(120)]
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Flag { get; set; }
        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; }
        public int UserId { get; set; }
        public List<TagEntity> Tags { get; set; } = new List<TagEntity>();

        [MaxLength(350)]
        public string Descritpion { get; set; }
        public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        public List<VoteEntity> UpVotes { get; set; } = new List<VoteEntity>();
       

    }
}