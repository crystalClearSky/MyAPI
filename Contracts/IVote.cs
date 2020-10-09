using System.Collections.Generic;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;

namespace MyAppAPI.Services
{
    public interface IVote
    {
         bool AddVote(Avatar avatar, int id);
         IEnumerable<Vote> GetVotesByAvatar(int id);

         void RemoveVote(Avatar avatar, int id);
         List<Vote> GetAllVotes();
    }
}