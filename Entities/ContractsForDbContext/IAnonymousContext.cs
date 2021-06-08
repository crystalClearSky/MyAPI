using System;

namespace Entities.ContractsForDbContext
{
    public interface IAnonymousContext
    {
        AnonymousEntity GetByAnonTime(DateTime anonFirstSeen);
        bool AddAnonymousUser(AnonymousEntity addNewAnon);
        bool UpdateAnon(AnonymousEntity anonToUpdate);
    }
}