using System.Security.Cryptography.X509Certificates;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using MyAppAPI.Services;
using MyAppAPI.AppRepository;
using System.ComponentModel.DataAnnotations;

namespace MyAppAPI.Models.GalleryModel
{
    public class GalleryCard
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Flag { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        [MaxLength(250)]
        public string Descritpion { get; set; }
        private IEnumerable<Comment> _commnents;
        public IEnumerable<Comment> Comments
        {
            get
            {
                _commnents = GetComments();
                if (_commnents == null)
                {
                    _commnents = new List<Comment>()
                     {
                         new Comment{ Id = -1 }
                     };
                }
                return _commnents;
            }
            set { _commnents = value; }
        }


        private int _upVotes;
        public int UpVotes
        {
            get
            {
                _upVotes = GetVotes();
                return _upVotes;
            }
            // set { _upVotes = value; }
        }
        // private readonly IAvatarData avatarDb;
        public GalleryCard()
        {

        }
        // public GalleryCard(IAvatarData avatarDb)
        // {
        //     this.avatarDb = avatarDb;
        // }

        public int GetVotes()
        {
            IAvatarData avatarDB = new AvatarData();
            var count = avatarDB.GetAllVotesForCard(this.Id);
            return count;
        }
        // Get comments for each GalleryCard ID
        public IEnumerable<Comment> GetComments()
        {
            ICommentData commentData = new CommentData();
            var comments = commentData.GetCommentsForEntity(this.Id);
            return comments;
        }
    }
}