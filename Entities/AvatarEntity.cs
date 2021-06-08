using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;
using Entities;

namespace MyAppAPI.Entities
{
    public class AvatarEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Temporary user account based on IP
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrentIP { get; set; }
        public int? GuestId { get; set; }
        [ForeignKey("GuestId")]
        public virtual GuestEntity Guest { get; set; }
        // checks if current avatar is online
        public bool IsCheckedIn { get; set; }
        public string AvatarImgUrl { get; set; }
        public List<UniqueVisitEntity> UniqueVisitEntity { get; set; }
        public DateTime JoinedOn { get; set; }
        public DateTime LastCheckedIn { get; set; }
        public List<NoticeEntity> Notices { get; set; } = new List<NoticeEntity>();
        public List<LikeEntity> Likes { get; set; } = new List<LikeEntity>();
        public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        public List<VoteEntity> UpVotes { get; set; } = new List<VoteEntity>();
        public List<ReplyEntity> Replies { get; set; } = new List<ReplyEntity>();
       
        public List<TagEntity> TagEntities { get; set; } = new List<TagEntity>();
        public FlagEntity FlagEntity { get; set; }
        public List<ViewEntity> ViewedContent { get; set; } = new List<ViewEntity>();

    }
}