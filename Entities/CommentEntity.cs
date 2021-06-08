using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Entities;
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
        // [ForeignKey("UserId")]
        // public virtual UserEntity UserEntity { get; set; }
        // public int? UserId { get; set; }

        [MaxLength(200)]
        public string Message { get; set; } = "Be the first to comment!";
        public DateTime LastUpdatedOn { get; set; }

        public DateTime PostedOn { get; set; }
        public int? ResponseToAvatarId { get; set; }
        //   public List<CommentEntity> Replies { get; set;}

        // [ForeignKey("CardId")]
        public int? ReplyToCommentId { get; set; }
        public  List<CommentEntity> RepliesTo { get; set; }
        [ForeignKey("ReplyToCommentId")]

        public virtual CommentEntity ReplyTo { get; set; }
        public CardEntity CardEntity { get; set; }
        public List<FlagEntity> Flags { get; set; } = new List<FlagEntity>();
        public List<LinkEntity> Links { get; set; } = new List<LinkEntity>();
        public int CardEntityId { get; set; }
        public bool IsReply { get; set; }
        public int SearchIndexValue { get; set; }
        public bool? IsSuperUser { get; set; }
        [ForeignKey("IsSuperUser")]
        public virtual UserEntity UserEntity { get; set; }

        // public int CardId { get; set; }
       //public List<ReplyEntity> Replies { get; set; } = new List<ReplyEntity>();
        public List<LikeEntity> Likes { get; set; } = new List<LikeEntity>();
        // public List<ReplyEntity> Response { get; set; } = new List<ReplyEntity>();
        public bool IsActive { get; set; } = false;
        public string FlaggedCommentMessages { get; set; }
        public Mediums Medium { get; set; }
    }
    public class CommentEntityMap : EntityTypeConfiguration<CommentEntity>
    {
        public CommentEntityMap()
        {
            HasOptional(c =>c.AvatarEntity);
            HasOptional(c =>c.UserEntity);
        }
    }

    public enum Mediums {
        [Description("Image")]
        Imaage,
        [Description("Video")]
        Video,
        [Description("Web Video")]
        WebVideo,
        [Description("Web Link")]
        WebLink,
        None
    }
}