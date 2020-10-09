using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAppAPI.AppRepository;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel.UpdateModel;
using MyAppAPI.Services;

namespace MyAppAPI.Controller
{
    [ApiController]
    [Route("api/content/avatar")]
    public class AvatarController : ControllerBase
    {
        private readonly IAvatarData avatarDb;
        private readonly ILogger _logger;

        public AvatarController(IAvatarData avatarDb, ILogger<AvatarController> logger)
        {
            this.avatarDb = avatarDb;
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        [HttpGet]
        public IActionResult GetAvatar()
        {
            try
            {
              var avatars = AvatarData.CurrentAvatar.GetAllAvatars();
              return Ok(avatars);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to retrieve any avatar data.", ex);
                return StatusCode(500, "There was an expection found.");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetAvatar(int id)
        {
            try
            {
              var avatar = AvatarData.CurrentAvatar.GetAvatarById(id);
              if (avatar == null)
              {
                  string message = $"The avatar with ID {id} does not exist!";
                  return NotFound(message);
              }
              return Ok(avatar);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve avatar ID: {id} data.", ex);
                return StatusCode(500, "There was an expection found.");
            }
        }

        // [HttpPut("{id}")]
        // public IActionResult UpdateAvatarActions(int id, [FromBody] UpdateAvatar updateAvatar)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest();
        //     }
        //     var avatar = AvatarData.CurrentAvatar.GetAvatarById(id);

        //     var finalAvatar = new Avatar()
        //     {
        //         Id = avatar.Id,
        //         CurrentIP = avatar.CurrentIP,
        //         Likes = updateAvatar.Likes,
        //         Comments = updateAvatar.Comments
        //     };

        // }

        [HttpDelete("{id}")]
        public IActionResult DeleteAvatar(int id)
        {
            try
            {
              var avatar = AvatarData.CurrentAvatar.GetAvatarById(id);
              if (avatar != null)
              {
                AvatarData.CurrentAvatar.DeleteAvatar(id);
                return Ok($"Avatar with ID: {id} has been delete.");
              }
              return NotFound($"Avatar ID: {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve avatar ID: {id} for deletion.", ex);
                return StatusCode(500, "There was an expection found.");
            }
            
        }
    }
}