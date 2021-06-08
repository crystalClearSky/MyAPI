using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using Models.ReMap;
using MyAppAPI.Entities;

namespace MyAppAPI.ReMap
{
    public class CardDto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [MinLength(2)]
        public string Title { get; set; }
        // [MinLength(10)]
        // public string ImageUrl { get; set; }
        public List<ImageEntity> Images { get; set; } = new List<ImageEntity>();
        public int ImageCount
        {
            get { return Images.Count; }
        }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        // [ForeignKey("UserId")]
        // public UserEntity UserEntity { get; set; }
        // public int UserId { get; set; }
        public string Content { get; set; }

        public bool IsSuperUser { get; set; }

        public List<TagDto> Tags { get; set; } = new List<TagDto>();

        [MaxLength(350)]
        public string Description { get; set; }
        public int searchIndexValue { get; set; }
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public List<UpVoteDto> UpVotes { get; set; } = new List<UpVoteDto>();
        public List<FlagDto> Flags { get; set; } = new List<FlagDto>();
        public bool TagIsActive { get; set; }
        public bool ReplyBoxIsActive { get; set; } = false;
        public List<ViewEntity> Views { get; set; } = new List<ViewEntity>();
        private int viewCount;
        public int ViewCount
        {
            get { return Views.Count; }
        }
        public PostType PostType { get; set; }
    }
}