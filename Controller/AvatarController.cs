using Microsoft.AspNetCore.Mvc;
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

        public AvatarController(IAvatarData avatarDb)
        {
            this.avatarDb = avatarDb;
        }

        [HttpGet]
        public IActionResult GetAvatar()
        {
            var avatars = AvatarData.CurrentAvatar.GetAllAvatars();
            return Ok(avatars);
        }
        [HttpGet("{id}")]
        public IActionResult GetAvatar(int id)
        {
            var avatar = AvatarData.CurrentAvatar.GetAvatarById(id);
            if (avatar == null)
            {
                string message = $"The avatar with ID {id} does not exist!";
                return NotFound(message);
            }
            return Ok(avatar);
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
            AvatarData.CurrentAvatar.DeleteAvatar(id);
            return NoContent();
        }
    }
}