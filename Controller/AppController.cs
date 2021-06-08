using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using System.Net;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.AppRepository;
using MyAppAPI.Models.GalleryModel;
using MyAppAPI.Services;
using MyAppAPI.Models;
using MyAppAPI.Entities.CreateCardEntity;
using MyAppAPI.Models.GalleryModel.UpdateModel;
using Microsoft.Extensions.Logging;
using MyAppAPI.Entities.ContractsForDbContext;
using MyAppAPI.Entities;
using MyAppAPI.Context;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using MyAppAPI.ReMap;
using MyAppAPI.Entities.UpdateCardEntity;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MyAppAPI.Entities.Simple;
using Entities;
using Entities.CreateCardEntity;

namespace MyAppAPI.Controller
{
    // get all comments here api/content/comment SuperUser Controll JObject to switch between
    [ApiController]
    [Route("api/content/")]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;
        private readonly IGalleryContext _ctx;
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

        public AppController(ILogger<AppController> logger, IGalleryContext ctx, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IActionResult GetContents(int skipBy = 0, int take = 5)
        {

            // Page and limits list.orderby(x =>x.item).skip(currentAmountSent(skipBy)).take(5)
            IEnumerable<CardEntity> cards = new List<CardEntity>();
            try
            {
                cards = _ctx.GetAllCards();
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to retrieve gallery cards data.", ex);
                return StatusCode(500, "There was an expection found.");
            }

            return Ok(_mapper.Map<IEnumerable<CardDto>>(cards));
        }

        [HttpGet("{id}", Name = "GetGallery")]
        public IActionResult GetContent(int id)
        {

            try
            {
                var result = _ctx.GetCardById(id);
                var card = _mapper.Map<CardDto>(result);

                if (card == null)
                {
                    string message = $"The data with id {id} was not found!";
                    return NotFound(message);
                }

                return Ok(card);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve content: {id}.", ex);
                return StatusCode(500, "There was an expection found at GetContent.");
            }
        }
        [HttpGet("setentry")]
        public IActionResult SetEntry()
        {
            return Ok();
        }
        public EnableOptionsEntity SetOptionsSession(EnableOptionsEntity createOptionSession)
        {
            HttpContext.Session.SetString("OptionsSession", JsonConvert.SerializeObject(createOptionSession));
            return _options;
        }

        [HttpPost("enableoption")]
        public IActionResult EnableOption(CreateEnableOptionsforRemap createOptions)
        {
            var optionResult = new EnableOptionsEntity();
            if (!string.IsNullOrEmpty(createOptions.IpAddress))
            {
                optionResult = _ctx.GetOption(createOptions.IpAddress);
            }
            else
            {
                optionResult = null;
            }
            if (optionResult == null && !string.IsNullOrEmpty(createOptions.IpAddress))
            {
                createOptions.FirstSeen = DateTime.Now;
                createOptions.LastSeen = DateTime.Now;
                if (_ctx.AddOption(_mapper.Map<EnableOptionsEntity>(createOptions)))
                {
                    var result = _ctx.GetOption(createOptions.IpAddress);
                    var finalResult = SetOptionsSession(result);
                    _logger.LogInformation("An option has been created and a session has been started");
                    return Ok(finalResult);
                }
                return BadRequest("Unable to create option");
            }

            if (_options == null)
            {
                if (createOptions.IsAnonymous)
                {
                    createOptions.FirstSeen = DateTime.Now;
                    createOptions.LastSeen = DateTime.Now;
                    if (_ctx.AddOption(_mapper.Map<EnableOptionsEntity>(createOptions)))
                    {
                        var result = _ctx.GetOption(seen: createOptions.FirstSeen);
                        var finalResult = SetOptionsSession(result);
                        _logger.LogInformation("An ANON option has been created and a session has been started");
                        return Ok(finalResult);
                    }
                    return BadRequest("Unable to create option");
                }
            }
            _logger.LogInformation($"This option for ip: {createOptions.IpAddress} already exists");
            return Ok(optionResult);
        }

        [HttpGet("enableoption/")]
        public IActionResult GetOption()
        {
            // if (_guest != null || _user != null)
            if (_options != null)
            {
                //var ip = _unregisteredGuest != null ? _unregisteredGuest.IPAddress : _guest != null ? _guest.IPAddress : _user != null ? _user.CurrentIP : "";
                if (_guest != null)
                {
                    _options.EnableGuest = true;
                    _options.IsUnregistered = false;
                    var res = _options;
                    HttpContext.Session.SetString("OptionsSession", JsonConvert.SerializeObject(res));

                }
                _logger.LogInformation($"option - {_options.EnableGuest}");
                var result = _ctx.GetOption(id: _options.Id);
                if (result != null)
                {
                    result.LastSeen = DateTime.Now;
                    result.VisitCount++;
                    if (_ctx.EditOption(result))
                    {
                        var final = _ctx.GetOption(id: _options.Id);
                        if (_options.IsMember) { _logger.LogInformation($"user OPTION."); }
                        if (_options.EnableGuest) { _logger.LogInformation($"guest OPTION."); }
                        if (_options.IsAnonymous) { _logger.LogInformation($"anonymous OPTION."); }
                        if (_options.IsUnregistered) { _logger.LogInformation($"unregistered Guest OPTION."); }
                        HttpContext.Session.SetString("OptionsSession", JsonConvert.SerializeObject(final));

                        _logger.LogInformation($"Get option Option Okayed.");
                        return Ok(_options);
                    }
                    return BadRequest("Unable to update option");
                }
                return BadRequest($"No Option was found.");


            }
            _logger.LogInformation("No guest was found to get option.");
            return Unauthorized("No guest was found to get option.");
        }

        [HttpPut("editoption")]
        public IActionResult EditOptions(EnableOptionsEntity editedOption)
        {
            int id = 0;

            var result = _ctx.GetOption(editedOption.IpAddress);
            if (result == null && _options != null)
            {
                id = _options.Id;
                result = _ctx.GetOption(id: id);
            }
            result.EnableGuest = editedOption.EnableGuest;
            result.IsMember = editedOption.IsMember;
            result.IsUnregistered = editedOption.IsUnregistered;
            result.IsAnonymous = editedOption.IsAnonymous;
            result.IsLiving = editedOption.IsLiving;
            result.LastSeen = DateTime.Now;
            result.VisitCount = 0;

            if (_ctx.EditOption(result))
            {
                SetOptionsSession(result);
                if (_options != null)
                {
                    _options.EnableGuest = editedOption.EnableGuest;
                    _options.IsMember = editedOption.IsMember;
                    _options.IsUnregistered = editedOption.IsUnregistered;
                    _options.IsAnonymous = editedOption.IsAnonymous;
                    _options.LastSeen = DateTime.Now;
                }
                else
                {
                    _logger.LogInformation("No Options found");
                }
                if (!_options.IsAnonymous)
                {
                    var finalOption = _ctx.GetOption(_options.IpAddress);
                    _logger.LogInformation("Option has been edited");
                    return Ok(finalOption);
                }
                _logger.LogInformation("Option ANONYMOUS has been edited");
                return Ok(_options);
            }
            return BadRequest("This Option could not be changed");
        }

        [HttpGet("optionbyip/")]
        public IActionResult GetOptionByIdOrIp(string IpAddress)
        {
            try
            {
                var result = _ctx.GetOption(ip: IpAddress);
                if (result == null)
                {
                    _logger.LogInformation($"No option with IP Address {IpAddress} was found");
                    return NotFound($"No option with IP Address {IpAddress} was found");
                }
                _logger.LogInformation("Get Option By Id was Successfull.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get a option by IP: {IpAddress}.", ex);
                return StatusCode(500, "There was an expection at GetOptionByIdOrIp.");
            }

        }
        [HttpGet("settings")]
        public IActionResult GetSettingByUser(int typeSelection = -1)
        {
            try
            {
                if (_user != null)
                {
                    return Ok();
                }
                _logger.LogInformation("Get Setting - No user is currently logged in");
                return Unauthorized("No user is currently logged in");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was an error found at Get Settings.", ex);
                return StatusCode(500, $"There was an error found in - {nameof(GetSettingByUser)}.");
            }
        }


        [HttpGet("tag/")]
        public IActionResult GetGalleryByTag([FromQuery] string q) // frombody list of tags
        {
            try
            {
                var tags = new List<TagEntity>();

                if (!string.IsNullOrWhiteSpace(q))
                {
                    var result = q.Split(' ');
                    foreach (var word in result)
                    {
                        tags.Add(new TagEntity() { TagItem = word });
                    }
                }
                else
                {
                    return BadRequest("Empty request!");
                }


                var cards = _ctx.GetGalleryCardsByTags(tags);
                if (cards.Count() <= 0)
                {
                    return NotFound("No Cards found!");
                }

                return Ok(cards);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get a gallery card with this tag.", ex);
                return StatusCode(500, "There was an expection at TAG Gallery Controller.");
            }
        }

        [HttpPost("{id}/images")]
        public IActionResult Addimages(int id, List<CreateImageForRemap> images)
        {

            try
            {
                var imageResults = new List<ImageEntity>();
                foreach (var image in images)
                {
                    image.CardEntityId = id;
                    image.UpdatedOn = DateTime.Now;
                    image.UpdatedOn = DateTime.Now;

                    imageResults.Add(_mapper.Map<ImageEntity>(image));
                }

                if (_ctx.AddImagesToCard(imageResults))
                {
                    return Ok();
                }
                return BadRequest("Nothing has been saved");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to add Images.", ex);
                return StatusCode(500, "There was an expection at AddImages.");
            }


        }

        [HttpGet("tags/")]
        public IActionResult GetTags(string tagQuery = "", int pageLimit = 4)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(tagQuery))
                    tagQuery = "";

                var result = _ctx.GetAllTags(tagQuery, pageLimit).OrderByDescending(c => c.CardsWithThisId);

                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("No tags where found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get Tags.", ex);
                return StatusCode(500, "There was an expection at Get Tags.");
            }
        }

        [AcceptVerbs("POST", "HEAD")]
        [HttpPost]
        public IActionResult CreateGalleryCard([FromBody] CreateGalleryForReMap createCard)
        {
            try
            {

                if (_admin != null && _admin.Id)
                {


                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    createCard.CreatedOn = DateTime.Now;
                    createCard.UpdatedOn = DateTime.Now;
                    createCard.IsSuperUser = _admin.Id;
                    foreach (var image in createCard.Images)
                    {
                        image.UpdatedOn = DateTime.Now;
                        image.UploadedOn = DateTime.Now;
                    }

                    foreach (var tag in createCard.Tags)
                    {
                        if (_admin != null)
                        {
                            tag.ByAvatarId = 1;
                        }
                        if (_user != null)
                        {
                            tag.ByAvatarId = _user.Id;
                        }
                    }
                    // var maxGalleryCardId = _ctx.GetAllCards().Max(g => g.Id);

                    _ctx.AddGalleryCard(_mapper.Map<CardEntity>(createCard));

                    var createdCard = _mapper.Map<CardEntity>(_mapper.Map<CardEntity>(createCard));

                    return CreatedAtRoute(
                        "GetGallery",
                        new { id = createdCard.Id },
                        createdCard
                    );
                }
                return Unauthorized("You do not have permission to create a card");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to CREATE a gallery card.", ex);
                return StatusCode(500, "There was an expection at CREATE Gallery Controller.");
            }

        }
        [HttpGet("current")]
        public IActionResult CurrentlyLoggedInAccount()
        {
            if (_user != null || _admin != null || _guest != null || _unregisteredGuest != null)
            {
                var result = new List<string>();
                result.Add(_user != null ? $"Avatar with IP: { _user.CurrentIP } is currently logged in." : "No AVATAR is logged in");
                result.Add(_admin != null ? $"Admin with name: { _admin.Name } is currently logged in." : "No ADMIN is logged in");
                result.Add(_guest != null ? $"Guest with IP: { _guest.IPAddress } is currently logged in." : "No GUEST is logged in");
                result.Add(_unregisteredGuest != null ? $"Un-registered Guest with IP: { _unregisteredGuest.IPAddress } is currently logged in." : "No UNREGISTERED GUEST is logged in");

                return Ok(result);
            }
            return BadRequest("No users are logged in.");
        }

        ///add getallcomment and likecomment
        [AcceptVerbs("PUT", "HEAD")]
        [HttpPut("{id}")]
        public IActionResult UpdateGalleryCard(int id,
        [FromBody] UpdateCardForReMap updateGalleryCard)
        {
            try
            {
                if (_admin != null && _admin.Id)
                {
                    int count = 0;
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var galleryCardStore = _ctx.GetCardById(id);
                    if (galleryCardStore == null)
                    {
                        return NotFound($"Card {id} was not found");
                    }

                    if (updateGalleryCard.Title != null)
                    {
                        galleryCardStore.Title = updateGalleryCard.Title;
                        count++;
                    }
                    // if (updateGalleryCard.ImageUrl != null)
                    // {
                    //     galleryCardStore.ImageUrl = updateGalleryCard.ImageUrl;
                    //     count++;
                    // }
                    if (updateGalleryCard.Images != null)
                    {
                        galleryCardStore.Images = updateGalleryCard.Images;
                        foreach (var image in updateGalleryCard.Images)
                        {
                            image.UpdatedOn = DateTime.Now;
                            count++;
                        }
                    }
                    if (updateGalleryCard.Tags != null)
                    {
                        foreach (var tags in updateGalleryCard.Tags)
                        {
                            // if (!_ctx.GetAllTags().Any(x => x.TagItem == tags.TagItem))
                            tags.ByAvatarId = null;
                        }
                        galleryCardStore.Tags = updateGalleryCard.Tags;
                        count++;
                    }
                    if (updateGalleryCard.Description != null)
                    {
                        galleryCardStore.Description = updateGalleryCard.Description;
                        count++;
                    }

                    if (updateGalleryCard.Content != null)
                    {
                        galleryCardStore.Content = updateGalleryCard.Content;
                        count++;
                    }

                    if (count > 0)
                    {
                        updateGalleryCard.IsSuperUser = _admin.Id;
                        galleryCardStore.UpdatedOn = DateTime.UtcNow;
                        if (!_ctx.UpdateGalleryCard(galleryCardStore))
                        {
                            return BadRequest($"Unable to save Updated for card:{id}.");
                        }
                    }

                    return Ok($"Gallery Card created: {galleryCardStore.Title}");
                }
                return BadRequest("You do not have permission to update this card.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to UPDATE a gallery card with ID: {id}.", ex);
                return StatusCode(500, "There was an expection at UPDATE Gallery Controller");
            }
        }

        [AcceptVerbs("HEAD")]
        [HttpGet("search/")]
        public IActionResult SearchForCard([FromQuery] string q, int limit = 3, int skipBy = 1, int postType = -1, int avatarId = -1, string typeSearch = "")
        {
            try
            {
                var cards = new List<CardEntity>();
                if (string.IsNullOrEmpty(q) && string.IsNullOrEmpty(typeSearch))
                {
                    cards = _ctx.GetAllCards(limit, skipBy, postType).OrderByDescending(c => c.Id).ToList();
                }
                else
                {
                    var queries = new List<string>();
                    if (!string.IsNullOrWhiteSpace(q))
                    {
                        var result = q.Split(' ');
                        foreach (var word in result)
                        {
                            queries.Add(word);
                        }
                    }
                    // else
                    // {
                    //     return BadRequest("Empty request!");
                    // }
                    cards = _ctx.SearchForCard(queries, limit, postType, skipBy, avatarId, typeSearch).OrderByDescending(c => c.Id).ToList();
                    if (cards == null)
                    {
                        return NotFound($"The was no comment found that containts {q}");
                    }
                }

                return Ok(cards);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get a comments with this term.", ex);
                return StatusCode(500, "There was an expection at SEARCH COMMENT Controller.");
            }
        }

        [AcceptVerbs("HEAD")]
        [HttpGet("{id}/comment/")]
        public IActionResult SearchComment(int id, [FromQuery] string q)
        {
            try
            {
                var queries = new List<string>();
                if (!string.IsNullOrWhiteSpace(q))
                {
                    var result = q.Split(' ');
                    foreach (var word in result)
                    {
                        queries.Add(word);
                    }
                }
                else
                {
                    queries = null;
                }
                var comments = _ctx.GetCommentWithTheseSearchTerms(queries);
                var finalResult = new List<CommentEntity>();
                if (id > 0)
                {
                    finalResult = comments.Where(c => c.CardEntityId == id).ToList();
                }
                else
                {
                    finalResult = comments.ToList();
                }
                if (finalResult.Count < 1)
                {
                    return NotFound($"No comment on card:{id} that contains ({q}) was found.");
                }

                return Ok(finalResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get a comments with this term.", ex);
                return StatusCode(500, "There was an expection at SEARCH COMMENT Controller.");
            }
        }

        [HttpGet("{id}/upvote")]
        public IActionResult AddUpVote(int id)
        {
            if (_user != null || _guest != null)
            {
                var upVote = new AddUpVote();
                if (_ctx.CardExists(id))
                {
                    var votes = _ctx.GetCardById(id).UpVotes.ToList();
                    var mappedVote = new VoteEntity();
                    // --- user vote ---->
                    if (_user != null && !votes.Any(v => v.VoteById == _user.Id))
                    {
                        upVote.CardId = id;
                        upVote.VoteById = _user.Id;
                        mappedVote = _mapper.Map<VoteEntity>(upVote);
                    }
                    if (_guest != null && !votes.Any(v => v.VoteByGuest == _guest.Id))
                    {
                        upVote.CardId = id;
                        upVote.VoteByGuest = _guest.Id;
                        mappedVote = _mapper.Map<VoteEntity>(upVote);
                    }

                    if (mappedVote.CardId != 0)
                    {
                        if (_ctx.AddOrRemoveUpVote(mappedVote))
                        {
                            return Ok($"An Upvoted has been ADDED on Post:{id}");
                        }
                    }
                    // --- guest vote --->

                    if (_user != null && votes.Any(v => v.VoteById == _user.Id))
                    {
                        var result = votes.FirstOrDefault(c => c.VoteById == _user.Id);
                        if (_ctx.AddOrRemoveUpVote(result, _user.Id))
                        {
                            return Ok($"An Upvoted has been REMOVED on Post:{id} by {_user.Name}");
                        }
                        return NotFound("No UpVote was found for REMOVAL for this user.");
                    }
                    if (_guest != null && votes.Any(v => v.VoteByGuest == _guest.Id))
                    {
                        var result = votes.FirstOrDefault(c => c.VoteByGuest == _guest.Id);
                        if (_ctx.AddOrRemoveUpVote(result, _guest.Id))
                        {
                            return Ok($"An Upvoted has been REMOVED on Post:{id} by GUEST from {_guest.Country}");
                        }
                        return NotFound("No UpVote was found for REMOVAL for this guest.");
                    }
                    return BadRequest($"You have already Upvoted on post: {id}.");
                }
                return NotFound($"Card:{id} was not found to upvote on.");
            }
            return Unauthorized("No user or guest is logged in.");
        }
        [HttpGet("{id}/view")]
        public IActionResult AddView(int id)
        {
            if (_user != null || _guest != null || _unregisteredGuest != null || _anonymous != null)
            {
                if (_ctx.CardExists(id))
                {
                    var views = _ctx.GetCardById(id).Views.ToList();
                    if ((_user != null && !views.Any(v => v.ViewedByAvatarId == _user.Id) || _unregisteredGuest != null && !views.Any(v => v.ViewedByUnregisteredGuest == _unregisteredGuest.Id) || _guest != null && !views.Any(v => v.ViewedByGuestId == _guest.Id) || _anonymous != null && !views.Any(v => v.AnonymousId == _anonymous.Id)))
                    {
                        var view = new CreateViewForRemap();
                        if (_user != null)
                            view.ViewedByAvatarId = _user.Id;
                        if (_guest != null)
                            view.ViewedByGuestId = _guest.Id;
                        if (_unregisteredGuest != null)
                            view.ViewedByUnregisteredGuest = _unregisteredGuest.Id;
                        if (_anonymous != null)
                            view.AnonymousId = _anonymous.Id;

                        view.CardEntityId = id;
                        view.NumberOfTimesSeen = 1;
                        view.FirstSeen = DateTime.Now;
                        view.LastSeen = DateTime.Now;

                        if (_ctx.AddView(_mapper.Map<ViewEntity>(view)) || _unregisteredGuest != null && view.ViewedByUnregisteredGuest < 0)
                        {
                            _logger.LogInformation($"View has been added to Card Id: {id}.");
                            return Ok($"View has been added to Card Id: {id}.");
                        }
                        else
                        {
                            _logger.LogInformation("View cannot be added.");
                            return BadRequest("View cannot be added.");
                        }

                    }
                    else
                    {
                        var view = _user != null ? _ctx.GetCardById(id).Views.FirstOrDefault(v => v.ViewedByAvatarId == _user.Id)
                        : _guest != null ? _ctx.GetCardById(id).Views.FirstOrDefault(v => v.ViewedByGuestId == _guest.Id)
                        : _unregisteredGuest != null ? _ctx.GetCardById(id).Views.FirstOrDefault(v => v.ViewedByUnregisteredGuest == _unregisteredGuest.Id)
                        : _anonymous != null ? _ctx.GetCardById(id).Views.FirstOrDefault(v => v.AnonymousId == _anonymous.Id)
                        : null;
                        if (view != null)
                        {
                            view.LastSeen = DateTime.Now;
                            view.NumberOfTimesSeen = view.NumberOfTimesSeen + 1;
                            if (_ctx.UpdateView(view))
                            {
                                return Ok($"View ID: {view.Id} has been updated");
                            }
                        }
                        else
                        {
                            return BadRequest("No view found and unable to update a view");
                        }
                        // update time viewed again
                    }
                }

                return BadRequest("This card does not exit");
            }
            return BadRequest("No user or anon is logged in");
        }



        [AcceptVerbs("DELETE")]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteGalleryCardAsync(int id)
        {
            try
            {
                if (_admin != null && _admin.Id)
                {
                    var galleryCard = _ctx.DeleteGalleryCardById(id);

                    if (galleryCard)
                    {
                        return Ok($"Gallery Card with Id: {id} has been deleted.");
                    }
                    return BadRequest($"The Gallery Card with ID: {id} does not exist.");
                }
                return BadRequest("You do not have permission to delete this card");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to DELETE ID: {id} gallery card.", ex);
                return StatusCode(500, "There was an expection at DELETE Gallery Controller");
            }
        }
    }
}