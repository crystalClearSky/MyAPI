using System.Xml.Linq;
using System.Linq;
using System;
using AutoMapper;
using Entities;
using Entities.ContractsForDbContext;
using Entities.CreateCardEntity;
using Entities.Simple;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using MyAppAPI.Entities;

namespace Controller
{
    [ApiController]
    [Route("api")]
    public class UnregisteredController : ControllerBase
    {
        private readonly IGuestContext _ctx;
        private readonly ILogger<UnregisteredController> _logger;
        private readonly IMapper _mapper;

        public UnregisteredController(IGuestContext ctx, ILogger<UnregisteredController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _ctx = ctx;
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

        public IActionResult SetupUnregisteredGuestSession([FromBody] UnregisteredGuestBasic data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("An IP Address is required.");
                }
                if (data != null)
                {
                    if (_ctx.GetAllUnregisteredGuest().Any(u => u.IPAddress == data.IPAddress))
                    {
                        //HttpContext.Session.Clear();
                        var theGuest = _mapper.Map<UnregisteredGuestSimple>(_ctx.GetAllUnregisteredGuest().FirstOrDefault(u => u.IPAddress == data.IPAddress));
                        HttpContext.Session.SetString("UnregisteredGuestSession", JsonConvert.SerializeObject(theGuest));
                        if (!(theGuest.FirstVisit.ToShortDateString() == DateTime.Now.ToShortDateString()))
                        {
                            theGuest.LastVisit = DateTime.Now;
                            if (!_ctx.UpdateUnregisteredGuest(_mapper.Map<UnregisteredGuestEnitity>(theGuest)))
                            {
                                return BadRequest("Unable to update Last Visit.");
                            }
                        }
                        _logger.LogInformation($"A UNREGISTERED GUEST session with IP: {theGuest.IPAddress} has been started.");
                        return Ok(_unregisteredGuest);
                    }
                    return BadRequest($"No UNREGISTERED GUEST with IP {data.IPAddress} was found.");
                }
                return BadRequest("No Valid UNREGISTERED GUEST data found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to create a session.", ex);
                return StatusCode(500, "The was and error found at SetupUnregisteredGuestSession");
            }
        }
        [HttpGet("test")]
        public IActionResult Test()
        {

            object cVFR = Request.Cookies["Key"];
            return Ok(cVFR);

        }
        // [HttpPost]
        // public IActionResult CreateNewGuest(CreateGuestForRemap createGuest)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest("Invalid Entry");
        //     }
        //     if (_ctx.Exist(createGuest.IPAddress))
        //     {
        //         return BadRequest("This guest already exists");
        //     }
        //     createGuest.FirstVisit = DateTime.Now;
        //     createGuest.LastVisit = DateTime.Now;
        //     if (_ctx.AddNewGuest(_mapper.Map<GuestEntity>(createGuest)))
        //     {
        //         var guest = new GuestBasic()
        //         {
        //             IPAddress = createGuest.IPAddress
        //         };

        //         SetGuestSession(data: guest);
        //         return Ok($"Guest from {createGuest.Region} has been added.");
        //     }

        //     return Ok();
        // }
        [HttpPost]
        public IActionResult AddUnregisteredGuest(CreateUnregisteredGuestForRemap addGuest)
        {
            try
            {
                if (_guest == null && _user == null && _options != null)
                {
                    if (ModelState.IsValid)
                    {
                        var guest = _ctx.GetAllUnregisteredGuest().Where(u => u.IPAddress == addGuest.IPAddress).FirstOrDefault();
                        if (guest == null)
                        {
                            addGuest.FirstVisit = DateTime.Now;
                            addGuest.LastVisit = DateTime.Now;
                            addGuest.EnableGuest = true;
                            _ctx.AddUnRegisteredGuest(_mapper.Map<UnregisteredGuestEnitity>(addGuest));
                            _logger.LogInformation("Unregisterd Guest listed.");
                            SetupUnregisteredGuestSession(data: _mapper.Map<UnregisteredGuestBasic>(addGuest));
                            return Ok("UNREGISTERED GUEST had been listed.");
                        }
                        // if unregistered guest exist count in visit here
                        guest.LastVisit = DateTime.Now;
                        _ctx.UpdateUnregisteredGuest(guest);
                        SetupUnregisteredGuestSession(data: _mapper.Map<UnregisteredGuestBasic>(addGuest));
                        var final = _ctx.GetAllUnregisteredGuest().FirstOrDefault(u => u.IPAddress == guest.IPAddress);
                        if (final.EnableGuest)
                        {
                            _logger.LogInformation($"{final.EnableGuest} this result");
                        }
                        
                        _logger.LogInformation("Unregisterd Guest is already registered.");
                        return Ok(final);
                    }
                    else
                    {
                        _logger.LogInformation("Invalid UNREGISTERED GUEST credetionals.");
                        return BadRequest("Invalid UNREGISTERED GUEST credetionals.");
                    }
                }
                else
                {
                    _logger.LogInformation("Unale to add unregistered guest");

                    return BadRequest($"A GUEST is already present.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to Add or Update UNREGISTERED GUEST.", ex);
                return StatusCode(500, "There was an expection at ADDUNREGISTERED GUEST at Unregistered Controller.");
            }

        }
        [HttpPut("enableguest")]
        public IActionResult EnableGuest(UnregisteredGuestBasic unregisteredGuest)
        {
            var result = _ctx.GetAllUnregisteredGuest().FirstOrDefault(u => u.IPAddress == unregisteredGuest.IPAddress);
            result.EnableGuest = true;
            if (_ctx.UpdateUnregisteredGuest(result))
            {
                var final = _ctx.GetAllUnregisteredGuest().FirstOrDefault(u => u.IPAddress == result.IPAddress);
                return Ok(final);
            }
            return BadRequest("Unable to ENABLE GUEST");
        }



    }
}