using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities;
using Entities.ContractsForDbContext;
using Microsoft.EntityFrameworkCore;
using MyAppAPI.Context;
using MyAppAPI.Entities;

namespace ContentRepository
{
    public class GuestDataRepo : IGuestContext
    {
        private readonly ContentContext _ctx;
        private readonly IMapper _mapper;

        public GuestDataRepo(ContentContext ctx, IMapper mapper)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public bool AddNewGuest(GuestEntity newGuest)
        {
            _ctx.GuestEntity.Add(newGuest);
            return Save();
        }

        public bool AddUnRegisteredGuest(UnregisteredGuestEnitity unregisteredGuest)
        {
            var result = GetAllUnregisteredGuest().FirstOrDefault(u => u.IPAddress == unregisteredGuest.IPAddress);
            if (result == null)
            {
                _ctx.UnregisteredGuestEnitity.Add(unregisteredGuest);
            }
            return Save();
        }

        public List<UnregisteredGuestEnitity> GetAllUnregisteredGuest()
        {
            var unregisteredGuests = _ctx.UnregisteredGuestEnitity.ToList();
            return unregisteredGuests;
        }

        public bool UpdateUnregisteredGuest(UnregisteredGuestEnitity guest)
        {
            if (guest != null)
            {
                _ctx.UnregisteredGuestEnitity.Attach(guest);
                _ctx.Entry(guest).State = EntityState.Modified;
            }
            return Save();
        }

        public bool AddOrRemoveVote(VoteEntity vote, int guestId = 0)
        {
            if (guestId > 0)
            {
                using (var context = new ContentContext())
                {
                    context.Remove(vote);
                    return context.SaveChanges() > 0;

                }
                //_ctx.Remove<VoteEntity>(Upvote);
            }

            if (guestId == 0)
            {
                using (var context = new ContentContext())
                {
                    context.Add(vote);
                    return context.SaveChanges() > 0;

                }
                //_ctx.Add<VoteEntity>(Upvote);
            }

            return Save();
        }

        public bool UpdateGuest(GuestEntity guestToUpdate)
        {
            _ctx.Attach(guestToUpdate);
            _ctx.Entry(guestToUpdate).State = EntityState.Modified;
            return Save();
        }

        public bool DeleteGuest(int id)
        {
            var guestToRemove = GetGuestByIpOrId(id: id);
            _ctx.GuestEntity.Remove(guestToRemove);
            return Save();
        }

        public bool Exist(string ipAddress = "", int id = -1)
        {
            var guest = new GuestEntity();
            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                guest = GetGuestByIpOrId(ipAddress: ipAddress);
            }
            if (id > 0)
            {
                guest = GetGuestByIpOrId(id: id);
            }
            return guest != null ? true : false;
        }

        public List<GuestEntity> GetAllGuest()
        {
            var guests = _ctx.GuestEntity.Include(v => v.Votes).ToList();
            return guests;
        }

        public GuestEntity GetGuestByIpOrId(string ipAddress = "", int id = -1)
        {
            var guest = GetAllGuest().FirstOrDefault(g => g.Id == id || g.IPAddress == ipAddress);
            return guest;
        }

        public bool Save()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}