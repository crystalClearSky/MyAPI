using System.Collections.Generic;
using MyAppAPI.Models;

namespace MyAppAPI.Services
{
    public interface ILike
    {
         List<Like> GetLikes(int id);
         bool AddLikeToContent(Avatar avatar, int id);
    }
}