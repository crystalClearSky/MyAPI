using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using MyAppAPI.AppRepository;
using MyAppAPI.Models.GalleryModel;
using MyAppAPI.Services;
using Newtonsoft.Json;
using System;

namespace MyAppAPI.Models
{
    // Get this to attach to an ip
    public class Avatar
    {

        private IEnumerable<Comment> _result { get; set; }
        // Temporary user account based on IP
        public int Id { get; set; }
        public string CurrentIP { get; set; }
        // checks if current avatar is online
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
                return Comments.Count();
            }
        }
        public List<Like> Likes { get; set; }
        // public List<Comment> Comments { get; set; }
        private IEnumerable<Comment> _comments;
        public IEnumerable<Comment> Comments
        {
            get
            {
                _comments = GetAllComments();
                if (_comments == null)
                {
                    _comments = new List<Comment>();
                }


                return _comments;
            }
            set { _comments = value; }
        }

        private IEnumerable<Comment> _repliesFromAvatars;
        public IEnumerable<Comment> RepliesFromAvatars
        {
            get
            {
                _repliesFromAvatars = GetAllReplies();
                return _repliesFromAvatars;
            }
            set { _repliesFromAvatars = value; }
        }


        public List<Vote> UpVotes { get; set; }

        public IEnumerable<Comment> GetAllComments()
        {
            ICommentData comments = new CommentData();
            _result = CommentData.CurrentComments.Comments.Where(c => c.AvatarId == this.Id);
            // var replies = comments.GetRepliesById(this.Id);
            // _result.Concat(replies);
            Sort();
            return _result;

        }

        public IEnumerable<Comment> GetAllReplies()
        {
            ICommentData comments = new CommentData();
            _result = comments.GetRepliesById(this.Id);

            Sort();
            return _result;
        }

        // public void AddRepliesToAvatar()
        // {
        //     ICommentData comments = new CommentData();
        //     var replies = comments.GetRepliesById(this.Id);

        //     foreach(var reply in replies)
        //     {
        //         this.Comments.Append(reply);
        //     }
        //     Sort();
        // }
        private void Sort()
        {
            _result.OrderBy(r => r.Id);
        }
    }
}