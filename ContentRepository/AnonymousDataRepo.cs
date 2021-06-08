using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities;
using Entities.ContractsForDbContext;
using Microsoft.EntityFrameworkCore;
using MyAppAPI.Context;

namespace ContentRepository
{
    public class AnonymousDataRepo : IAnonymousContext
    {
        private readonly ContentContext _ctx;
        private readonly IMapper _mapper;
        public AnonymousDataRepo(ContentContext ctx, IMapper mapper)
        {
            _mapper = mapper;
            _ctx = ctx;
        }

        public bool AddAnonymousUser(AnonymousEntity addNewAnon)
        {
            _ctx.AnonymousEntity.Add(addNewAnon);
            return Save();
        }

        public List<AnonymousEntity> GetAllAnons()
        {
            var anons = _ctx.AnonymousEntity.ToList();
            return anons;
        }

        public AnonymousEntity GetByAnonTime(DateTime anonFirstSeen)
        {
            var anon = _ctx.AnonymousEntity.FirstOrDefault(a => a.FirstSeen == anonFirstSeen);
            return anon;
        }

        public bool UpdateAnon(AnonymousEntity anonToUpdate)
        {
            _ctx.AnonymousEntity.Attach(anonToUpdate);
            _ctx.Entry(anonToUpdate).State = EntityState.Modified;
            return Save();
        }

        private bool Save() => _ctx.SaveChanges() > 0;
    }
}