using System.Linq;
using Entities;
using Microsoft.Extensions.Logging;
using MyAppAPI.Context;
using Entities.ContractsForDbContext;
using MyAppAPI.Entities;
using System;

namespace ContentRepository
{
    public class ContactDataRepo : IContactContext
    {
        private readonly ContentContext _ctx;
        public ContactDataRepo(ContentContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public bool SendEmail(ContactEntity message)
        {
            _ctx.ContactEntity.Add(message);
            return Save();
        }

        public ContactEntity GetEmail(ContactEntity contact)
        {
            var result = _ctx.ContactEntity.FirstOrDefault(c => c.FirstName == contact.FirstName);
            if (result.TimeSent.ToShortTimeString() == contact.TimeSent.ToShortTimeString())
            {
                return result;
            }
            return null;
        }

        public bool Save() => _ctx.SaveChanges() > 0;
    }
}