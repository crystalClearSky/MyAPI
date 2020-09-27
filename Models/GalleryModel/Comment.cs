using System;
using System.Collections.Generic;
using System.Linq;
using MyAppAPI.AppRepository;
using MyAppAPI.Services;

namespace MyAppAPI.Models.GalleryModel
{
    public class Comment
    {
        public int Id { get; set; }
        public int AvatarId { get; set; }
        public string Message { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LikesCount
        {
            get
            {
                return Likes.Count();
            }
        }
        public List<Like> Likes { get; set; } = new List<Like>();
        public int ReplyToCommentId { get; set; }
        public int ReplyCount
        {
            get
            {
                return Replies.Count;
            }
        }
        public DateTime PostedOn { get; set; }
        // List replies

        //Check if like already exists
        private IList<Comment> _replies;
        public IList<Comment> Replies
        {
            get
            {
                _replies = GetRepliesForThisComment();
                if (_replies == null)
                {
                    _replies = new List<Comment>();
                }
                return _replies.ToList();
            }
            set { _replies = value; }
        }
        public int CommentOnCardId { get; set; }


        public IList<Comment> GetRepliesForThisComment()
        {
            ICommentData commentData = new CommentData();
            var comments = new List<Comment>();
            var reply = commentData.GetRepliesById(this.Id);
            comments = comments.Concat(reply).ToList();

            return comments;
        }
    }
}