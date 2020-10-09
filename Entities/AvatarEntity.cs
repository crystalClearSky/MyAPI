using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;

namespace MyAppAPI.Entities
{
    public class AvatarEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Temporary user account based on IP
        public int? Id { get; set; }
        public string CurrentIP { get; set; }
        // checks if current avatar is online
        public bool IsCheckedIn { get; set; }
        public string AvatarImgUrl { get; set; }
        public DateTime JoinedOn { get; set; }

        public List<LikeEntity> Likes { get; set; } = new List<LikeEntity>();
        public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        public List<VoteEntity> UpVotes { get; set; } = new List<VoteEntity>();
        public List<ReplyEntity> Replies { get; set; } = new List<ReplyEntity>();
       
        public List<TagEntity> TagEntities { get; set; } = new List<TagEntity>();
    }
}