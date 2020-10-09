using System.Collections.Generic;
using MyAppAPI.Models;

namespace MyAppAPI.Services
{
    // rename to contract folder instead of services
    public interface IAvatarData
    {
         Avatar GetAvatarById(int id);
        //  int GetAllVotesForCard(int id);
        //  int GetAllLikesForCard(int id);
         IEnumerable<Avatar> GetAllAvatars();
         void AddNewAvatar(string newIp);
         void DeleteAvatar(int id);
    }
}