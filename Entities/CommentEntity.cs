using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyAppAPI.Models;

namespace MyAppAPI.Entities
{
    public class CommentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("AvatarId")]
        public virtual AvatarEntity AvatarEntity { get; set; }
        public int? AvatarId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity UserEntity { get; set; }
        public int? UserId { get; set; }

        [MaxLength(200)]
        public string Message { get; set; }
        public DateTime LastUpdatedOn { get; set; }

        public DateTime PostedOn { get; set; }
        //   public List<CommentEntity> Replies { get; set;}

        [ForeignKey("CardId")]
        public CardEntity CardEntity { get; set; }

        public bool IsSuperUser { get; set; }
        public int CardId { get; set; }
        public List<ReplyEntity> Replies { get; set; } = new List<ReplyEntity>();
        public List<LikeEntity> Likes { get; set; } = new List<LikeEntity>();

        public ReplyEntity Response { get; set; }

    }
    public class CommentEntityMap : EntityTypeConfiguration<CommentEntity>
    {
        public CommentEntityMap()
        {
            HasOptional(c =>c.AvatarEntity);
            HasOptional(c =>c.UserEntity);
        }
    }
}