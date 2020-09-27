using System.Collections.Generic;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;

namespace MyAppAPI.Services
{
    public interface ICommentData
    {
         IEnumerable<Comment> GetAllComment();
         Comment GetCommentById(int id);

        //  Comment AddComment();

         IEnumerable<Comment> GetCommentsByAvatar(int avatarId);

         IEnumerable<Comment> GetRepliesById(int avatarReplyId);

         IEnumerable<Comment> GetCommentsForEntity(int id);

        //  List<Comment> GetCommentsByAvatar(Avatar avatar);
         void AddComment(Comment comment);

         void DeleteComment(int id);
         
    }
}