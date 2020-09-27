using System.Linq;
using System.Collections.Generic;
using MyAppAPI.Models;

namespace MyAppAPI.AppRepository
{
    public class LikeData
    {
        public static LikeData CurrentLikes { get; } = new LikeData();
        public List<Like> Likes { get; set; } // if null this will equal to new List<like>
        public LikeData()
        {
            Likes = new List<Like>()
            {
                new Like()
                {
                    Id = 1,
                    HasLiked = true,
                    LikedById = 1
                },
                // new Like()
                // {
                //     Id = 2,
                //     HasLiked = true,
                //     LikedById = 3
                // },
                // new Like()
                // {
                //     Id = 3,
                //     HasLiked = true,
                //     LikedById = 2
                // },
                // new Like()
                // {
                //     Id = 4,
                //     HasLiked = true,
                //     LikedById = 3
                // },
                // new Like()
                // {
                //     Id = 5,
                //     HasLiked = true,
                //     LikedById = 1
                // },
                // new Like()
                // {
                //     Id = 6,
                //     HasLiked = true,
                //     LikedById = 3
                // }


            };
        }
        // public bool AddLikeToContent(Avatar avatar, int commentId)
        // {
        //     bool isValid = false;
        //     var comment = CommentData.CurrentComments.GetCommentById(commentId);
        //     var commentLikes = comment.Likes;
        //     var maxLike = LikeData.CurrentLikes.Likes.Max(l => l.Id) + 1;
        //     if (!(commentLikes.Any(c => c.LikedById == avatar.Id)))
        //     {
        //         comment.Likes.Add(new Like() { Id = maxLike, HasLiked = true, LikedById = avatar.Id });
        //         isValid = true;
        //     }
        //     return isValid;
        // }
    }
}