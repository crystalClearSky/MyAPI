using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.AppRepository;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;
using MyAppAPI.Services;

namespace MyAppAPI.Controller
{
    [ApiController]
    [Route("api/content/avatar/{avatarId}/comment")]
    public class CommentsController : ControllerBase
    {
        private readonly IAvatarData AvatarDb;
        private readonly ICommentData CommentDb;

        public CommentsController(IAvatarData avatarDb, ICommentData commentDb)
        {
            this.AvatarDb = avatarDb;
            this.CommentDb = commentDb;
        }

        [HttpGet]
        public IActionResult GetComments(int avatarId)
        {
            var avatar = AvatarDb.GetAvatarById(avatarId);
            var comments = avatar.Comments.OrderBy(c => c.Id);
            if (comments == null)
            {
                var message = $"No avatar with ID {avatarId} was found.";
                return NotFound(message);
            }

            return Ok(comments);
        }
        [HttpGet("{id}", Name = "GetComment")]
        public IActionResult GetComments(int avatarId, int id)
        {
            var avatar = AvatarDb.GetAvatarById(avatarId);
            var comments = avatar.Comments.OrderBy(c => c.Id);
            if (comments == null)
            {
                var message = $"No avatar for ID {avatarId} was found.";
                return NotFound(message);
            }
            var comment = comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
            {
                var message = $"No comment with Id {id} was found.";
                return NotFound(message);
            }

            return Ok(comment);
        }
        [HttpPost]
        public IActionResult CreateComment(int avatarId, [FromBody] CommentCreation comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var avatar = AvatarDb.GetAvatarById(avatarId);
            var comments = CommentDb.GetAllComment();
            if (avatar == null)
            {
                var message = $"Could not find avatar with ID {avatarId}";
                return NotFound(message);
            }
            var maxCommentId = CommentDb.GetAllComment().Max(c => c.Id);

            var finalComment = new Comment()
            {
                Id = ++maxCommentId,
                AvatarId = avatar.Id,
                Message = comment.Message,
                PostedOn = DateTime.UtcNow,
            };
            CommentData.CurrentComments.AddComment(finalComment);
            // CommentDb.AddComment(finalComment);
            return CreatedAtRoute(
                "GetComment",
                new { avatarId, id = finalComment.Id },
                finalComment
            );
        }
        [HttpPut("{id}")]
        public IActionResult UpdateComment(int avatarId, int id, [FromBody] CommentUpdate comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var avatar = AvatarDb.GetAvatarById(avatarId);
            if (avatar == null)
            {
                var message = $"Could not find avatar with ID {avatarId}";
                return NotFound(message);
            }

            var commentStore = CommentData.CurrentComments.GetCommentById(id);
            if(comment == null)
            {
                return NotFound("Data must be added for update.");
            }

            commentStore.Message = comment.Message;
            commentStore.LastUpdatedOn = DateTime.UtcNow;

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int avatarId, int id)
        {
            // var avatar = AvatarDb.GetAvatarById(avatarId);
            // if (avatar == null)
            // {
            //     var message = $"Could not find avatar with ID {avatarId}";
            //     return NotFound(message);
            // }

            // var commentStore = CommentData.CurrentComments.GetCommentById(id);
            // if(commentStore == null)
            // {
            //     var message = $"No comment with ID:{id} was found!";
            //     return NotFound(message);
            // }

            CommentData.CurrentComments.DeleteComment(id);

            return NoContent();

        }
    }
}