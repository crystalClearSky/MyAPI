using System.Net.Http.Headers;
using System.Linq;
using System.Collections.Generic;
using MyAppAPI.Models.GalleryModel;
using MyAppAPI.Services;
using MyAppAPI.Models;

namespace MyAppAPI.AppRepository
{
    public class CommentData : ICommentData, ILike
    {
        public static CommentData CurrentComments { get; set; } = new CommentData();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public CommentData()
        {
            Comments = new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    AvatarId = 2,
                    CommentOnCardId = 2,
                    Message = "This is a great piece of work. keep it up!"
                },
                new Comment()
                {
                    Id = 2,
                    AvatarId = 1,
                    CommentOnCardId = 1,
                    Message = "I think this needs a little more effort",
                    Likes = new List<Like>()
                    {
                        new Like()
                        {
                            Id = 4,
                            HasLiked = true,
                            LikedById = 3
                        },
                    }
                },
                new Comment()
                {
                    Id = 3,
                    AvatarId = 2,
                    CommentOnCardId = 3,
                    Message = "Wow!! Now this is something special!",
                    Likes = new List<Like>()
                    {
                        new Like()
                        {
                            Id = 1,
                            HasLiked = true,
                            LikedById = 1
                        },
                        new Like()
                        {
                            Id = 3,
                            HasLiked = true,
                            LikedById = 2
                        }
                    },

                },
                new Comment()
                {
                    Id = 4,
                    AvatarId = 3,
                    CommentOnCardId = 3,
                    Message = "Where did you learn how to do this?!",
                    Likes = new List<Like>()
                    {
                        new Like()
                        {
                            Id = 5,
                            HasLiked = true,
                            LikedById = 1
                        }
                    }
                },
                new Comment()
                {
                    Id = 5,
                    AvatarId = 3,
                    CommentOnCardId = 2,
                    Message = "I really like how you did the hair on this character."
                },
                new Comment()
                {
                    Id = 6,
                    AvatarId = 1,
                    CommentOnCardId = 2,
                    Message = "Nice work!",
                    Likes = new List<Like>()
                    {
                        new Like()
                        {
                            Id = 6,
                            HasLiked = true,
                            LikedById = 3
                        }
                    }
                },
                new Comment()
                {
                    Id = 7,
                    AvatarId = 2,
                    CommentOnCardId = 1,
                    Message = "I totally agree with you!. FOR AVATAR ONE!!",
                    ReplyToCommentId = 2,
                    Likes = new List<Like>()
                    {
                        new Like()
                        {
                            Id = 7,
                            HasLiked = true,
                            LikedById = 1
                        }
                    }
                },
                new Comment()
                {
                    Id = 8,
                    AvatarId = 3,
                    CommentOnCardId = 1,
                    Message = "What do you mean by AVATAR ONE!??",
                    ReplyToCommentId = 7
                },
                new Comment()
                {
                    Id = 9,
                    AvatarId = 1,
                    CommentOnCardId = 1,
                    Message = "I'm Glad you agree!!!",
                    ReplyToCommentId = 7,
                    Likes = new List<Like>()
                    {
                        new Like()
                        {
                            Id = 2,
                            HasLiked = true,
                            LikedById = 3
                        }
                    }
                },
                new Comment()
                {
                    Id = 10,
                    AvatarId = 2,
                    CommentOnCardId = 2,
                    Message = "Ya! Jungle Woman sounds like an interest name for a movie.",
                    ReplyToCommentId = 5
                }
            };

        }

        public IEnumerable<Comment> GetAllComment()
        {
            return Comments.OrderBy(c => c.Id);
        }

        public Comment GetCommentById(int id)
        {
            return Comments.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Comment> GetCommentsByAvatar(int avatarId)
        {
            var avatarComments = Comments.Where(c => c.AvatarId == avatarId);
            return avatarComments;
        }

        public IEnumerable<Comment> GetRepliesById(int Id)
        {
            var replyComments = Comments.Where(c => c.ReplyToCommentId == Id);

            return replyComments;
        }

        public IEnumerable<Comment> GetCommentsForEntity(int id)
        {
            return Comments.Where(c => c.CommentOnCardId == id && c.ReplyToCommentId <= 0);
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public void DeleteComment(int id)
        {
            var commnentToDelete = Comments.FirstOrDefault(c =>c.Id == id);
            if (commnentToDelete == null)
            {
                return;
            }
            Comments.Remove(commnentToDelete);
        }

        public List<Like> GetLikes(int id)
        {
            var commentsWithLikesByUser = CommentData.CurrentComments.
            Comments.Where(c => c.Likes.Any(l => l.LikedById == id));
            var Likes = new List<Like>();
            foreach (var comment in commentsWithLikesByUser)
            {
                Likes.AddRange(comment.Likes);
            }

            // var count = commentsWithLikesByUser.Count();

            return Likes;
        }

        public List<Like> GetAllLikes()
        {
            var comments = CommentData.CurrentComments.Comments;
            var likes = comments.Select(c => c.Likes);
            var result = new List<Like>();
            foreach (var like in likes)
            {
                foreach (var l in like)
                {
                    result.Add(l);
                }
                // var l = like.Where(l => l.LikedById == 1);
            }

            return result;
        }
        public bool AddLikeToContent(Avatar avatar, int commentId)
        {
            bool isValid = false;
            var comment = CommentData.CurrentComments.GetCommentById(commentId);
            var commentLikes = comment.Likes;
            var allLikes = CommentData.CurrentComments.GetAllLikes();
            // var maxLike = LikeData.CurrentLikes.Likes.Max(l => l.Id) + 1;
            var maxLike = allLikes.Max(l => l.Id) + 1;
            if (!(commentLikes.Any(c => c.LikedById == avatar.Id)))
            {
                comment.Likes.Add(new Like() { Id = maxLike, HasLiked = true, LikedById = avatar.Id });
                isValid = true;
            }
            return isValid;
        }
    }
}