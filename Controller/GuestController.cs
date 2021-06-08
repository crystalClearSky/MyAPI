using System;
using AutoMapper;
using Entities;
using Entities.ContractsForDbContext;
using Entities.CreateCardEntity;
using Entities.Simple;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.ReMap;
using MyAppAPI.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Controller
{
    [ApiController]
    [Route("api/content/guest")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestEntity> _logger;
        private readonly IGuestContext _ctx;
        private readonly IMapper _mapper;

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

        public GuestController(ILogger<GuestEntity> logger, IGuestContext ctx, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetGuests()
        {
            var result = _ctx.GetAllGuest();
            return Ok(result);
        }
        [HttpGet("{id?}/{ip?}")]
        public IActionResult GetGuest(int id = -1, string ip = "") {
            var guest = _ctx.GetGuestByIpOrId(ip, id);
            _logger.LogInformation($"Attemping to retrieve a guest - id:{guest.Id}");
            return Ok(guest);
        }
        [HttpPost("set/")]
        public IActionResult SetGuestSession([FromBody] GuestBasic data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("An IP Address is required.");
                }
                if (data != null)
                {
                    if (_options != null) { _logger.LogInformation($"OPTION IS PRESENT is still present"); }

                    if (_ctx.Exist(data.IPAddress))
                    {
                        var theGuest = _mapper.Map<GuestSimple>(_ctx.GetGuestByIpOrId(data.IPAddress));
                        HttpContext.Session.SetString("GuestSession", JsonConvert.SerializeObject(theGuest));
                        if (!(theGuest.FirstVisit.ToShortDateString() == DateTime.Now.ToShortDateString()))
                        {
                            theGuest.LastVisit = DateTime.Now;
                            if (!_ctx.UpdateGuest(_mapper.Map<GuestEntity>(theGuest)))
                            {
                                _logger.LogInformation("Unable to update Last Visit.");
                                return BadRequest("Unable to update Last Visit.");
                            }
                        }
                        _logger.LogInformation($"A guest session with IP: {theGuest.IPAddress} has been started.");
                        if (_options != null) {_logger.LogInformation($"{_options.FirstSeen} is still present"); }
                        return Ok($"A session for guest ip: {theGuest.IPAddress} has successfully started.");
                    }
                    _logger.LogInformation("No guest with IP {data.IPAddress} was found.");
                    return BadRequest($"No guest with IP {data.IPAddress} was found.");
                }
                _logger.LogInformation("No Valid guest data found.");
                return BadRequest("No Valid guest data found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to create a session.", ex);
                return StatusCode(500, "The was and error found at SetGuestSesstion");
            }
        }
        [HttpPost]
        public IActionResult CreateNewGuest(CreateGuestForRemap createGuest)
        {
            if (_user == null && _options != null)
            {
                if (_options.EnableGuest)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest("Invalid Entry");
                    }
                    if (_ctx.Exist(createGuest.IPAddress))
                    {
                        var result = _ctx.GetGuestByIpOrId(createGuest.IPAddress);
                        _logger.LogInformation("ReAdding GUEST");
                        return Ok(result);
                    }
                    createGuest.FirstVisit = DateTime.Now;
                    createGuest.LastVisit = DateTime.Now;
                    if (_ctx.AddNewGuest(_mapper.Map<GuestEntity>(createGuest)))
                    {
                        var guest = new GuestBasic()
                        {
                            IPAddress = createGuest.IPAddress
                        };
    
                        SetGuestSession(data: guest);
                        var final = _ctx.GetGuestByIpOrId(guest.IPAddress);
                        
                        _logger.LogInformation("ADDING GUEST");
                        return Ok(final);
                    }
                }
                _logger.LogInformation("Not set as a MEMBER is OPTIONS");
                return BadRequest("Not set as a MEMBER is OPTIONS");
            }
            return BadRequest($" is currently logged in.");
        }
        [HttpGet("i")]
        public IActionResult GetAction()
        {
            return Ok(_guest);
        }

        [HttpGet("logoutguest")]
        public IActionResult LogThisGuestOut()
        {
            if (_options != null)
            {
                if (_options.IsMember)
                {
                    HttpContext.Session.Remove("GuestSession");
                    if (_guest == null)
                    {
                        _logger.LogInformation("Guest has been removed");
                        return Ok("Guest has been removed");
                    }
                    _logger.LogInformation("Unable to remove guest");
                    return BadRequest("Unable to remove guest");
                }
                return BadRequest("Uble to remove GUEST, OPTION IS NOT MEMBER YET");
            }
            return BadRequest("Unable to remove GUEST, OPTION is NULL");

        }
    }
}