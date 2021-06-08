using System;
using AutoMapper;
using Entities;
using Entities.ContractsForDbContext;
using Entities.CreateCardEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAppAPI.Entities;
using Newtonsoft.Json;

namespace Controller
{
    [ApiController]
    [Route("api/anon")]
    public class AnonymousController : ControllerBase
    {
        private readonly ILogger<AnonymousController> _logger;
        private readonly IMapper _mapper;
        private readonly IAnonymousContext _ctx;

        private UnregisteredGuestEnitity _unregisteredGuest
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UnregisteredGuestSession")))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<UnregisteredGuestEnitity>(HttpContext.Session.GetString("UnregisteredGuestSession"));
            }
        }
        
        private AnonymousEntity _anonymous
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("AnonymousSession")))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<AnonymousEntity>(HttpContext.Session.GetString("AnonymousSession"));
            }
        }
        private GuestEntity _guest
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("GuestSession")))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<GuestEntity>(HttpContext.Session.GetString("GuestSession"));
            }
        }
        private AvatarEntity _user
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserSession")))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<AvatarEntity>(HttpContext.Session.GetString("UserSession"));
            }
        }
        private EnableOptionsEntity _options
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("OptionsSession")))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<EnableOptionsEntity>(HttpContext.Session.GetString("OptionsSession"));
            }
        }
        public AnonymousController(IAnonymousContext ctx, ILogger<AnonymousController> logger, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAndSetAnAnon()
        {
            if (_guest == null && _user == null && _options == null && _unregisteredGuest == null)
            {
                if (_anonymous == null)
                {
                    var createAnon = new CreateAnonymousforRemap()
                    {
                        FirstSeen = DateTime.Now,
                        LastSeen = DateTime.Now
                    };
                    if (_ctx.AddAnonymousUser(_mapper.Map<AnonymousEntity>(createAnon)))
                    {
                        SetSessionForAnon(data: createAnon);
                        _logger.LogInformation("A new Anon has been added and a session has been set.");
                        return Ok(_anonymous);
                    }
                    else
                    {
                        return BadRequest("Could no add new Anon");
                    }
                }
                var anon = _anonymous;
                anon.LastSeen = DateTime.Now;
                if (_ctx.UpdateAnon(anon))
                {
                    var newAnon = _ctx.GetByAnonTime(anon.FirstSeen);
                    return Ok(newAnon);
                }
                return BadRequest("An anon is already present.");
            }
            else
            {
                _logger.LogInformation($"Something is already present");
                return BadRequest($"Something is already present");
            }
        }
        public IActionResult SetSessionForAnon(CreateAnonymousforRemap data)
        {
            if (data != null)
            {
                var result = _ctx.GetByAnonTime(data.FirstSeen);
                if (result != null)
                {
                    HttpContext.Session.SetString("AnonymousSession", JsonConvert.SerializeObject(result));
                    if (!(result.FirstSeen.ToShortDateString() == DateTime.Now.ToShortDateString()))
                    {
                        
                            return Ok("A session has been start for anon");
                        
                    }
                }
                return BadRequest("This anon does not exist");

            }
            return BadRequest("Anon data is null");
        }
    }
}