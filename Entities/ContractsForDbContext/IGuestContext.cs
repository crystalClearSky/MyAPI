using System.Collections.Generic;
using MyAppAPI.Entities;

namespace Entities.ContractsForDbContext
{
    public interface IGuestContext
    {
        bool UpdateUnregisteredGuest(UnregisteredGuestEnitity guest);
        bool AddUnRegisteredGuest(UnregisteredGuestEnitity unregisteredGuest);
        List<UnregisteredGuestEnitity> GetAllUnregisteredGuest();
        bool AddNewGuest(GuestEntity newGuest);
        GuestEntity GetGuestByIpOrId(string ipAddress = "", int id = -1);
        List<GuestEntity> GetAllGuest();
        bool AddOrRemoveVote(VoteEntity vote, int guestId = 0);
        bool Exist(string ipAddress = "", int id = -1);
        public bool UpdateGuest(GuestEntity guestToUpdate);
        bool DeleteGuest(int id);
        bool Save();
    }
}