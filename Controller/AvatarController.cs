using System.Threading;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAppAPI.Entities;
using MyAppAPI.Entities.ContractsForDbContext;
using MyAppAPI.Entities.CreateCardEntity;
using MyAppAPI.ReMap;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MyAppAPI.Entities.Simple;
using System.Collections.Generic;
using Entities;
using Entities.ContractsForDbContext;
using Microsoft.Extensions.Configuration;

namespace MyAppAPI.Controller
{
    [ApiController]
    [Route("api/content/user")]
    public class AvatarController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAvatarContext _ctx;
        private readonly IGuestContext _guestContext;
        private readonly IMapper _mapper;
        private CookieOptions opt { get; set; } = new CookieOptions { SameSite = SameSiteMode.None };
        private DateTime current { get; set; }
        // Change id to fruit up 16
        private int counter { get; set; }
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
        private UserEntity _admin
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("AdminSession")))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<UserEntity>(HttpContext.Session.GetString("AdminSession"));
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

        public static int _timer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public AvatarController(ILogger<AvatarController> logger, IAvatarContext ctx, IGuestContext guestContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentException(nameof(httpContextAccessor));
            _ctx = ctx ?? throw new ArgumentException(nameof(ctx));
            _guestContext = guestContext ?? throw new ArgumentNullException(nameof(guestContext));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
        [HttpGet("sessionstate")]
        public IActionResult CheckSeesionState()
        {
            int count = 0;
            CookieOptions cookie = new CookieOptions()
            {
                Expires = new DateTimeOffset(DateTime.Now.AddSeconds(30)),
                HttpOnly = true,
                SameSite = SameSiteMode.None
            };
            if (!Request.Cookies.ContainsKey("Now"))
            {
                Response.Cookies.Append("Now", counter.ToString(), cookie);
                //return Content("Welcome, new visitor!");
            }
            else
            {
                int add = Int32.Parse(Request.Cookies["Now"]) + 1;
                Response.Cookies.Append("Now", add.ToString(), cookie);
                count = Int32.Parse(Request.Cookies["Now"]);

            }
            if (_user != null && Request.Cookies["Key"] == null)
            {

                if (count < 2)
                {
                    var t = new
                    {
                        name = "John",
                        surname = "smith"
                    };
                    Set("Key", JsonConvert.SerializeObject(t), 20);
                    _logger.LogInformation($"Minisession is running {Request.Cookies["Key"]}");
                    return Ok($"Minisession is running {cookie.Expires}");
                }
            }
            if (Request.Cookies["Key"] == null)
            {
                HttpContext.Session.Remove("UserSession");
                _logger.LogInformation("User session has been cleared");
                return BadRequest("user has been logged out");

            }
            else
            {
                _logger.LogInformation("A session is still present");
                return Ok("A session is still present");
            }
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            if (Request.Cookies["Key"] != null)
            {
                string cVFR = Request.Cookies["Key"];
                return Ok(JsonConvert.DeserializeObject(cVFR));
            }
            return NotFound("No Session was found!");
        }

        [HttpGet("test2")]
        public IActionResult Test2()
        {
            opt.Expires = new DateTimeOffset(DateTime.Now.AddMinutes(1));
            Response.Cookies.Append("Key", Request.Cookies["Key"], opt);
            return Ok($"here{opt.Expires}");
        }


        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            try
            {
                if (_user != null || _admin != null)
                {
                    if (_user != null)
                    {
                        var avatar = _ctx.GetAvatarById(_user.Id);
                        if (avatar != null)
                        {
                            return Ok(avatar);
                        }
                        return BadRequest($"No content was found for {_user.Id}");
                    }
                    if (_admin != null)
                    {
                        return Ok(_admin);
                    }
                }
                return Unauthorized("No user is current logged in;");
            }
            catch (Exception ex)
            {
                _logger.LogError(Messages(WarningType.LogMessage, "LOGGIN"), ex);
                return StatusCode(500, Messages(WarningType.ExceptionError, nameof(GetCurrentUser)));
            }
        }

        [HttpGet("all/{option:int?}")]
        public IActionResult GetAllAvatar(int option = 0)
        {
            try
            {
                var avatars = _ctx.GetAllAvatars(option);
                if (avatars == null)
                {
                    return NotFound("No Avatar was found");
                }
                return Ok(avatars);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to retrieve any avatar data.", ex);
                return StatusCode(500, Messages(WarningType.ExceptionError, nameof(GetAllAvatar)));
            }
        }

        [HttpPost("set/")]
        public IActionResult SetSession([FromBody] JObject data)
        {
            try
            {
                // Authenication will be done here!!
                if (data["admin"] != null)
                {
                    var admin = data["admin"].ToObject<UserEntity>();
                    var adminDetails = new UserEntity();
                    _config.GetSection(adminDetails.Admin).Bind(adminDetails);
                    if (admin.Name == adminDetails.Name)
                    {
                        HttpContext.Session.Remove("UserSession");
                        HttpContext.Session.SetString("AdminSession", JsonConvert.SerializeObject(data["admin"]));
                        _logger.LogInformation($"Admin: {admin.Name} has successfully signed in.");
                        return Ok($"A seesion has been set for {admin.Name.ToUpper()} 10MIN or TILL LOGOUT!");
                    }
                    _logger.LogInformation("Invalid detail.");
                    return Unauthorized("Invalid details");
                }

                // if (data["guest"] != null)
                // {
                //     var guest = data["guest"].ToObject<GuestEntity>();
                //     if (_guestContext.Exist(guest.IPAddress))
                //     {
                //         var theGuest = _mapper.Map<AvatarSimple>(_guestContext.GetGuestByIpOrId(guest.IPAddress));
                //         HttpContext.Session.SetString("GuestSession", JsonConvert.SerializeObject(data["admin"]));
                //         if (!(guest.FirstVisit.ToShortDateString() == DateTime.Now.ToShortDateString()))
                //         {
                //             guest.LastVisit = DateTime.Now;
                //             if (_guestContext.UpdateGuest(guest))
                //             {
                //                 _logger.LogInformation($"A guest session with IP: {guest.IPAddress} has been started.");
                //                 return Ok($"A session for guest ip: {guest.IPAddress} has successfully started.");
                //             }
                //         }
                //         return BadRequest("Unfortunate a seesion could not be started for GUEST");
                //     }
                //     return BadRequest($"No guest with IP {guest.IPAddress} was found.");
                // }


                // get user via repo
                if (data["user"] != null)
                {
                    var user = data["user"].ToObject<AvatarEntity>();
                    if (_ctx.Exist(user.CurrentIP))
                    {
                        HttpContext.Session.Remove("AdminSession");
                        var avatar = _mapper.Map<AvatarSimple>(_ctx.GetAvatarById(ip: user.CurrentIP));
                        HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(avatar));
                        if (!(avatar.JoinedOn.ToShortDateString() == DateTime.Now.ToShortTimeString()))
                        {
                            if (!avatar.IsCheckedIn)
                            {
                                if (!_ctx.CheckIn(avatar.Id))
                                {
                                    return BadRequest($"Unable to Check In {_user.Name}.");
                                }
                            }
                        }
                        //SessionTimer();
                        _logger.LogInformation($"Username: {avatar.Name} session has been created.");
                        return Ok($"User: {avatar.Name} has checked in at {avatar.LastCheckedIn} and seesion has been set for 10MIN or TILL LOGOUT! ");
                    }
                }
                return BadRequest("No USER has been found for setup. Defaulting to Anonymous.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to create a session.", ex);
                return StatusCode(500, Messages(WarningType.ExceptionError, nameof(SetSession)));
            }

        }

        [HttpGet("search")]
        public IActionResult SearchForUser([FromQuery] string q, int limit = 3)
        {
            _logger.LogInformation("Timere extended by 10 seconds.");
            var queries = new List<string>();
            if (!(string.IsNullOrWhiteSpace(q)))
            {
                var result = q.Split(' ');
                foreach (var word in result)
                {
                    queries.Add(word);
                }
            }
            else
            {
                return BadRequest("Empty request for user search");
            }
            var avatars = _ctx.GetUserSearchResult(queries, limit);
            if (avatars != null)
            {
                return Ok(_mapper.Map<IEnumerable<AvatarDto>>(avatars));
            }
            return NotFound($"No user with query {q} was found.");
            //var avatarResult

            // Create user here!! in future
            // var session = JsonConvert.DeserializeObject<UserEntity>(HttpContext.Session.GetString("UserSession"));
            // return Ok(session);
        }

        [HttpGet("{id:int?}/{ip?}")]
        public IActionResult GetAvatar(int id = 0, string ip = "")
        {
            try
            {
                var avatar = _ctx.GetAvatarById(id, ip);
                if (avatar == null)
                {
                    string message = Messages(WarningType.UserNotFoundError, userType: id.ToString());
                    return NotFound(message);
                }
                var result = _mapper.Map<AvatarDto>(avatar);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(Messages(WarningType.LogMessage, "RETRIEVE", id.ToString()), ex);
                return StatusCode(500, Messages(WarningType.ExceptionError, nameof(GetAvatar)));
            }
        }

        [HttpGet("fruits")]
        public IActionResult GetRemainingFruits() {
            var result = _ctx.GetRemainingFruit();
            return Ok(result);
        }

        [AcceptVerbs("POST")]
        [HttpPost]
        public IActionResult CreateNewAvatar([FromBody] CreateAvatarForRemap createAvatar)
        {
            try
            {
                _logger.LogInformation("IN CREATE AVATAR.");
                if (!ModelState.IsValid)
                {
                    // Checks if required properties are valid
                    return BadRequest("Invalid Entry");
                }
                if (!_ctx.Exist(createAvatar.CurrentIP))
                {
                    _logger.LogInformation("IN CREATE AVATAR - creating.");

                    createAvatar.JoinedOn = DateTime.Now;
                    createAvatar.IsCheckedIn = true;
                    createAvatar.LastCheckedIn = DateTime.Now;
                    var newGuest = new GuestEntity();
                    if (_unregisteredGuest != null && _options.IsUnregistered)
                    {
                        _logger.LogInformation("IN CREATE AVATAR - _unregisteredGuest.");
                        newGuest.IPAddress = _unregisteredGuest.IPAddress;
                        newGuest.Region = _unregisteredGuest.Region;
                        newGuest.Country = _unregisteredGuest.Country;
                        newGuest.FirstVisit = _unregisteredGuest.FirstVisit;
                        newGuest.LastVisit = _unregisteredGuest.LastVisit;
                        if (!_guestContext.AddNewGuest(newGuest))
                        {
                            return BadRequest("There was a error found while adding new guest at create new avatar.");
                        }

                    }
                    if (_guest != null && _options.IsMember)
                    {
                        _logger.LogInformation("IN CREATE AVATAR - _guest.");
                        newGuest.IPAddress = _guest.IPAddress;
                        newGuest.Region = _guest.Region;
                        newGuest.Country = _guest.Country;
                        newGuest.FirstVisit = _guest.FirstVisit;
                        newGuest.LastVisit = _guest.LastVisit;
                        if (!_guestContext.AddNewGuest(newGuest))
                        {
                            return BadRequest("There was a error found while adding new guest at create new avatar.");
                        }
                    }
                    var createdGuest = _guestContext.GetAllGuest().FirstOrDefault(g => g.IPAddress == (createAvatar.CurrentIP));
                    if (createdGuest != null)
                    {
                        createAvatar.GuestId = createdGuest.Id;
                        createAvatar.Votes = createdGuest.Votes;
                    }

                    var avatar = _mapper.Map<AvatarEntity>(createAvatar);
                    var json = string.Empty;
                    if (_ctx.AddNewAvatar(avatar))
                    {
                        json = @"{'user':{'currentIP':" + $"'{avatar.CurrentIP}'" + "}}";
                        var obj = new JObject();
                        obj = JObject.Parse(json);

                        var result = SetSession(data: obj);
                        var finalAvatar = _ctx.GetAvatarById(ip: createAvatar.CurrentIP);
                        _logger.LogInformation($"New User {avatar.Name} has been created and signed in on {DateTime.Now}");
                        return Ok(finalAvatar);

                    }
                    return BadRequest("The user already exists");
                }
                // Add redirect to route
                return BadRequest("Nothing was created.");
            }
            catch (Exception ex)
            {
                _logger.LogError(Messages(WarningType.LogMessage, reference: "CREATE"), ex);
                return StatusCode(500, Messages(WarningType.ExceptionError, nameof(CreateNewAvatar)));
            }
        }
        [AcceptVerbs("PUT")]
        [HttpPut]
        public IActionResult UpdateAvatarActions(int id, [FromBody] UpdateAvatarSimple updateAvatar)
        {
            try
            {
                if (_user != null)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                    var avatar = _ctx.GetAvatarById(_user.Id);
                    int count = 0;
                    var finalAvatar = new AvatarEntity();
                    if (updateAvatar.AvatarImgUrl != null)
                    {
                        avatar.AvatarImgUrl = updateAvatar.AvatarImgUrl;
                        count++;
                    }
                    if (count < 1)
                    {
                        return BadRequest("No changes have been made.");
                    }
                    if (_ctx.UpdateAvatar(avatar))
                    {
                        return Ok(Messages(WarningType.Success, "UPDATED", _user.Id.ToString()));
                    }
                }
                return Unauthorized(Messages(WarningType.PermissionError, reference: "UPDATE", id.ToString()));
            }
            catch (Exception ex)
            {
                _logger.LogError(Messages(WarningType.LogMessage, reference: "DELETE"), ex);
                return StatusCode(500, Messages(WarningType.ExceptionError, nameof(UpdateAvatarActions)));
            }

        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {

            try
            {
                if (HttpContext.Session.IsAvailable)
                {
                    string message = string.Empty;
                    if (_user != null)
                    {
                        message = _ctx.LogOutUser(_user.Id);
                        if (string.IsNullOrWhiteSpace(message)) return NotFound("Cannot find user to logout");
                    }
                    else
                    {
                        message = "admin has been logged out";
                    }

                    HttpContext.Session.Remove("UserSession");
                    HttpContext.Session.Remove("AdminSession");
                    if (_options != null) { _logger.LogInformation($"{_options.IpAddress}"); }
                    _logger.LogInformation($"User has been logged out");
                    return Ok(message);
                }
                return BadRequest("No User is current logged in");
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to logout. The may be not user logged in.", ex);
                return StatusCode(500, Messages(WarningType.ExceptionError, nameof(LogOut)));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAvatar(int id)
        {
            try
            {
                if (_admin != null && _admin.Id || _user != null && _user.Id == id)
                {
                    if (_ctx.DeleteAvatar(id))
                    {
                        return Ok(Messages(WarningType.Success, "DELETED", id.ToString()));
                    }
                    return NotFound(Messages(WarningType.UserNotFoundError, userType: _user.Id.ToString()));
                }

                return Unauthorized(Messages(WarningType.PermissionError, "DELETE", id.ToString()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve avatar ID: {id} for deletion.", ex);
                return StatusCode(500, Messages(WarningType.ExceptionError, nameof(DeleteAvatar)));
            }

        }

        public string Messages(WarningType opt, string reference = "", string userType = "")
        {
            var message = string.Empty;
            switch (opt)
            {
                case WarningType.UserSeessionExpired:
                    message = $"No user is logged for {reference}.";
                    break;
                case WarningType.Success:
                    message = $"User: {userType} has been {reference}.";
                    break;
                case WarningType.ExceptionError:
                    message = $"There was an expection found at {reference}.";
                    break;
                case WarningType.UserNotFoundError:
                    message = $"User: {userType} was not found.";
                    break;
                case WarningType.PermissionError:
                    message = $"You do not have permisison to {reference} ID:{userType}.";
                    break;
                case WarningType.LogMessage:
                    message = $"Unable to {reference} user{userType}.";
                    break;
                default:
                    break;
            }
            return message;
        }
        [Route("Method")]
        public void Set(string key, string value, int? expireTime)
        {
            //CookieOptions option = new CookieOptions();
            if (!opt.Expires.HasValue)
            {
                opt.Expires = new DateTimeOffset(DateTime.Now.AddSeconds(expireTime.Value));
            }
            else
                opt.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, opt);

            _logger.LogInformation($"{opt.Expires}");
            _logger.LogInformation($"{opt.Expires}");
        }
    }
}