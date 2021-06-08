using System;
using System.Linq;
using System.Net.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAppAPI.Entities.ContractsForDbContext;

namespace Controller
{
    [ApiController]
    [Route("api/comments")]
    public class JustCommentsController : ControllerBase
    {
        private readonly ILogger<JustCommentsController> _logger;
        private readonly IAvatarContext _ctx;
        public JustCommentsController(ILogger<JustCommentsController> logger, IAvatarContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult GetAllComments()
        {
            try
            {
                var result = _ctx.GetAllComment().Result.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve all comments.", ex);
                return StatusCode(500, "There was an expection found at GetAllComments.");
            }
        }

        [HttpGet("comment_count")]
        public IActionResult GetCommentCount(int avatarId = 0, int contentId = 0)
        {
            var result = _ctx.GetTotalCommentCountCount(avatarId, contentId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentsByCard(int id, int commentPageSize = 4, int commentPageNumber = 1)
        {
            try
            {
                var result = _ctx.GetAllCommmentsByCard(id, commentPageSize, commentPageNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve data for content id: { id }", ex);
                return StatusCode(500, "There was an exception found at GetCommentsBycard.");
            }
        }
    }
}