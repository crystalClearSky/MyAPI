using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Entities;
using MyAppAPI.Entities;

namespace MyAppAPI.ReMap
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int? AvatarId { get; set; }
        [MaxLength(200)]
        public string Message { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LikesCount
        {
            get
            {
                return Likes.Count;
            }
        }
        public DateTime PostedOn { get; set; }
        public int? ResponseToAvatarId { get; set; }

        public int CardEntityId { get; set; }
        public bool IsReply { get; set; }
        public bool? IsSuperUser { get; set; }
        public int? ReplyToCommentId { get; set; }
        public string FlaggedCommentMessages { get; set; }
        public List<LinkEntity> Links { get; set; } = new List<LinkEntity>();
        public List<LikeDto> Likes { get; set; } = new List<LikeDto>();
        public List<ReplyDto> Response { get; set; } = new List<ReplyDto>();
        public List<FlagEntity> Flags { get; set; } = new List<FlagEntity>();
        public List<CommentEntity> RepliesTo { get; set; }
        public Mediums Medium { get; set; }


        public bool IsActive { get; set; }
        public int ReplyCount
        {
            get
            {
                return RepliesTo.Count;
            }
        }

        public int LinkCount
        {
            get
            {
                return Links.Count;
            }
        }

        public enum Mediums
        {
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
}