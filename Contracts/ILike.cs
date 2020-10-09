using System.Collections.Generic;
using MyAppAPI.Models;

namespace MyAppAPI.Services
{
    public interface ILike
    {
         List<Like> GetLikesByAvatar(int id);
         bool AddLikeToContent(Avatar avatar, int id);
         public List<Like> GetAllLikes();
    }
}