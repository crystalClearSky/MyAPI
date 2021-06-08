using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAppAPI.Entities.ContractsForDbContext;
using AutoMapper;
using MyAppAPI.ReMap;
using MyAppAPI.Entities.CreateCardEntity;
using MyAppAPI.Entities;
using MyAppAPI.Entities.UpdateCardEntity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using MyAppAPI.Entities.Simple;
using System.Text.RegularExpressions;
using Entities.Simple;

namespace MyAppAPI.Controller
{
    // create route where you create, deleting and find all avatars api/avatar
    [ApiController]
    [Route("api/content/{contentId}/user/comment")]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAvatarContext _ctx;
        private readonly IMapper _mapper;
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


        public CommentsController(ILogger<CommentsController> logger, IAvatarContext ctx, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetComments(int contentId)
        {
            try
            {
                var comments = new List<CommentEntity>();
                if (_user != null)
                {
                    if (contentId == 0)
                    {
                        var dbResult = _ctx.GetCommentsByAvatar(_user.Id);
                        comments = dbResult.OrderBy(c => c.Id).ToList();
                    }
                    if (contentId > 0)
                    {
                        var results = _ctx.GetAllComment().Result;
                        comments = results.Where(c => c.CardEntityId == contentId).ToList();
                    }

                    if (comments == null)
                    {
                        var message = $"No comments for user:{_user.Id} was found.";
                        return NotFound(message);
                    }
                    var result = _mapper.Map<IEnumerable<CommentDto>>(comments);

                    return Ok(result);
                }

                return Unauthorized("Unable get comments. " + Message("unautherized"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve comments by user: {_user.Id}.", ex);
                return StatusCode(500, "There was an expection found at GetComments.");
            }
        }

        [HttpGet("{id}", Name = "GetComment")]
        public IActionResult GetComment(int id)
        {
            // super
            try
            {
                var avatar = _ctx.GetAvatarById(_user.Id);

                var comments = avatar.Comments.OrderBy(c => c.Id);
                var comment = comments.FirstOrDefault(c => c.Id == id);

                if (comment == null)
                {
                    var message = $"No comment with Id {id} was found.";
                    return NotFound(message);
                }

                return Ok(_mapper.Map<CommentDto>(comment));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve comment: {id} by user: {_user.Id}.", ex);
                return StatusCode(500, "There was an expection found at GetComment.");
            }
        }
        [AcceptVerbs("POST", "HEAD")]
        [HttpPost]
        public IActionResult CreateComment(int contentId, [FromBody] CreateCommentForRemap createdComment)
        {
            try
            {

                if (_user != null || _admin != null)
                {

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    if (createdComment.Message.Contains('#') || createdComment.Message.Contains('@'))
                    {
                        var user = new AvatarEntity();
                        var sentence = " " + createdComment.Message;
                        int id = _user.Id;   /*(int)createdComment.AvatarId; */
                        var result = _ctx.DetectObjectType(sentence, contentId, id);
                        // if(result != null)
                        // {
                        //     user = (AvatarEntity)result[0];
                        //     _logger.LogInformation($"This is the result - {user.Name} - {user.Id}");
                        // }
                    }

                    if (_admin != null)
                    {
                        createdComment.IsSuperUser = _admin.Id;
                        createdComment.AvatarId = null;
                    }
                    if (_user != null)
                    {
                        createdComment.IsSuperUser = null;
                        createdComment.AvatarId = _user.Id;
                    }
                    createdComment.LastUpdatedOn = DateTime.Now;
                    createdComment.PostedOn = DateTime.Now;
                    createdComment.CardEntityId = contentId;
                    var date = createdComment.PostedOn;
                    var finalComment = new CommentEntity();
                    _ctx.AddCommentByUser(_mapper.Map<CommentEntity>(createdComment)); //
                    if (_admin != null)
                    {
                        var dbResult = _ctx.GetCommentsByAvatar(admin: _admin.Id);
                        finalComment = dbResult.Where(p => p.PostedOn == date).FirstOrDefault();
                    }
                    if (_user != null)
                    {
                        var dbResult = _ctx.GetCommentsByAvatar(id: _user.Id);
                        finalComment = dbResult.Where(p => p.PostedOn == date).FirstOrDefault();
                    }
                    return CreatedAtRoute(
                        "GetComment",
                        new { contentId, id = finalComment.Id },
                        finalComment
                    );
                }
                return BadRequest("No User is Logged in");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to create a comment on card: {contentId}.", ex);
                return StatusCode(500, "There was an expection found at CreateComment.");
            }
        }

        [HttpGet("{id}/like")]
        public IActionResult LikeComment(int id)
        {
            // if (_user != null)
            // {


            //     if (!like.Any(l => l.LikedById == _user.Id) && comment.AvatarId == id)
            //     {
            //         addLike.CommentId = id;
            //         addLike.LikedById = _user.Id;
            //         var mappedLike = _mapper.Map<LikeEntity>(addLike);
            //         if (await _ctx.AddOrRemoveLike(mappedLike))
            //         {
            //             return Ok($"Comment: {id} was liked by {_user.Name}");
            //         }
            //         return BadRequest($"Comment: {id} has already been liked by {_user.Name}.");
            //     }

            //     return BadRequest($"No like was added to comment: {id}.");
            // }
            // return NotFound("No User has Logged in.");

            if (_user != null)
            {
                var addLike = new AddLikeDto();
                var comment = _ctx.GetCommentById(id);
                if (comment == null)
                {
                    NotFound($"No comment has been found with id {id}");
                }
                var like = comment.Likes.ToList();
                if (!like.Any(v => v.LikedById == _user.Id))
                {
                    addLike.CommentId = id;
                    addLike.LikedById = _user.Id;
                    var mappedLike = _mapper.Map<LikeEntity>(addLike);
                    if (_ctx.AddOrRemoveLike(mappedLike))
                    {
                        return Ok($"An LIKE has been ADDED on comment:{id} by {_user.Name}");
                    }
                }
                if (like.Any(v => v.LikedById == _user.Id))
                {
                    var result = like.FirstOrDefault(c => c.LikedById == _user.Id);
                    if (_ctx.AddOrRemoveLike(result, _user.Id))
                    {
                        return Ok($"A like has been REMOVED on comment:{id} by {_user.Name}");
                    }
                    return NotFound("No LIKE was found for REMOVAL.");
                }
                return BadRequest($"You have already LIKED comment: {id}.");
            }
            return Unauthorized(Message("unautherized"));
        }


        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, [FromBody] UpdateCommentDto updatedComment)
        {
            try
            {
                if (_user != null || _admin != null)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var commentStore = new CommentEntity();
                    var dbResult = new List<CommentEntity>();
                    if (_admin != null)
                    {
                        var result = _ctx.GetCommentsByAvatar(admin: _admin.Id);
                        commentStore = dbResult.Where(c => c.Id == id).FirstOrDefault();
                    }
                    else
                    {
                        if (updatedComment.FlaggedCommentMessages != null)
                        {
                            dbResult = _ctx.GetAllComment().Result.ToList();
                        }
                        else
                        {
                            dbResult = _ctx.GetCommentsByAvatar(_user.Id).ToList();
                        }
                    }
                    commentStore = dbResult.Where(c => c.Id == id).FirstOrDefault();
                    updatedComment.LastUpdatedOn = DateTime.Now;
                    _mapper.Map(updatedComment, commentStore);

                    if (_ctx.UpdateContent(commentStore))
                    {
                        return Ok($"Comment with ID: {id} has been updated.");
                    }
                    return BadRequest("No update was made");
                }
                return Unauthorized(Message("unautherized"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to Update comment: {id}.", ex);
                return StatusCode(500, "There was an expection found at UpdateComment.");
            }
        }

        [HttpPost("{id}/flag")]
        public IActionResult AddOrRemoveFlag(int id, [FromBody] FlagBasic flagBasic)
        {
            if (_user != null)
            {

                if (_ctx.GetAllComment().Result.Any(c => c.Id == id))
                {
                    var flag = new FlagDto();
                    var flags = _ctx.GetCommentById(id).Flags.ToList();
                    if (!flags.Any(v => v.AvatarId == _user.Id))
                    {
                        if (!ModelState.IsValid)
                        {
                            return BadRequest("Invalid message entry");
                        }
                        flag.ReasonText = flagBasic.ReasonText;
                        flag.CommentEntityId = id;
                        flag.AvatarId = _user.Id;
                        var mappedFlag = _mapper.Map<FlagEntity>(flag);
                        if (_ctx.AddOrRemoveFlag(mappedFlag))
                        {
                            return Ok($"An Upvoted has been ADDED on Post:{id} by {_user.Name}");
                        }
                    }
                    if (flags.Any(v => v.AvatarId == _user.Id))
                    {
                        var result = flags.FirstOrDefault(c => c.AvatarId == _user.Id);
                        if (_ctx.AddOrRemoveFlag(result, _user.Id))
                        {
                            return Ok($"An Upvoted has been REMOVED on Post:{id} by {_user.Name}");
                        }
                        return NotFound("No UpVote was found for REMOVAL.");
                    }
                }
            }
            return Unauthorized("No user is logged in.");
        }

        [HttpPost("{id}")]
        public IActionResult ReplyToComment(int contentId, int id, [FromBody] CreateCommentForRemap reply)
        {
            // super
            try
            {
                if (_user != null || _admin != null)
                {
                    if (reply.Message.Contains('#') || reply.Message.Contains('@'))
                    {
                        var user = new AvatarEntity();
                        var sentence = " " + reply.Message;
                        int userid = (int)reply.AvatarId;
                        var results = _ctx.DetectObjectType(sentence, contentId, userid);
                        // if(result != null)
                        // {
                        //     user = (AvatarEntity)result[0];
                        //     _logger.LogInformation($"This is the result - {user.Name} - {user.Id}");
                        // }
                    }

                    if (_user != null)
                    {
                        reply.AvatarId = _user.Id;
                    }

                    if (_admin != null)
                    {
                        reply.IsSuperUser = _admin.Id;
                    }
                    var comment = _ctx.GetCommentById(id);

                    reply.PostedOn = DateTime.Now;
                    reply.CardEntityId = contentId;
                    reply.ReplyToCommentId = id;
                    reply.ResponseToAvatarId = comment.AvatarId;
                    var result = _mapper.Map<CommentEntity>(reply);
                    if (_user != null)
                    {
                        if (_ctx.AddCommentByUser(result, _user.Id, commentIdToAddOn: id))
                        {
                            return Ok($"Reply had been added by USER: {_user.Id} comment:{id}.");
                        }
                    }
                    if (_admin != null)
                    {
                        if (_ctx.AddCommentByUser(result, admin: _admin.Id, commentIdToAddOn: id))
                        {
                            return Ok($"Reply had been added by ADMIN on comment:{id}.");
                        }
                    }
                    return NotFound($"No REPLY has been added to comment ID:{id}");
                }
                return Unauthorized(Message("unautherized"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to create reply on comment: {id}.", ex);
                return StatusCode(500, "There was an expection found at RepltToComment.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                if (_user != null)
                {
                    if (_ctx.DeleteCommentByAvatar(_user.Id, commentId: id))
                    {
                        return Ok($"Comment:{id} by user:{_user.Id} has been delete.");
                    }
                }

                if (_admin != null)
                {
                    if (_ctx.DeleteCommentByAvatar(admin: _admin.Id, commentId: id))
                    {
                        // add attribute to delete any user's post
                        _logger.LogInformation($"Comment:{id} has been deleted by {_admin.Name}");
                        return Ok($"Comment:{id} by {_admin.Name} has been delete.");
                    }
                }

                return Unauthorized($"No user is logged in.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve comment: {id} for deletion.", ex);
                return StatusCode(500, "There was an expection found at DeleteComment.");
            }

        }
        public string Message(string message)
        {
            string result = string.Empty;
            switch (message)
            {
                case "unautherized":
                    result = "No user has been logged in.";
                    break;

                default:
                    break;
            }
            return result;
        }
    }
}