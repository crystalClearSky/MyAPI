using System;
using System.Collections.Generic;
using Entities;

namespace MyAppAPI.ReMap
{
    public class AvatarDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrentIP { get; set; }
        public bool IsCheckedIn { get; set; }
        public string AvatarImgUrl { get; set; }
        public DateTime JoinedOn { get; set; }
        public int TotalLikes
        {
            get
            {
                return Likes.Count;
            }
        }
        public int TotalUpVotes
        {
            get
            {
                return UpVotes.Count;
            }
        }
        public int TotalComments
        {
            get
            {
                return Comments.Count;
            }
        }
        public List<LikeDto> Likes { get; set; } = new List<LikeDto>();
        public List<UpVoteDto> UpVotes { get; set; } = new List<UpVoteDto>();
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public List<ReplyDto> Replies { get; set; } = new List<ReplyDto>();
        public List<TagDto> Tags { get; set; } = new List<TagDto>();
        public FlagDto Flag { get; set; }
        public List<ViewEntity> ContentViewed { get; set; } = new List<ViewEntity>();
        private int contentViewCount;
        public int ContentViewCount
        {
            get { return ContentViewed.Count; }
        }
        
        
    }
}