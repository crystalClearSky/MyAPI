using System;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.Context;

namespace MyAppAPI.Controller
{
    [ApiController]
    [Route("api/testdatabase")]
    public class DummyController : ControllerBase
    {
        private readonly ContentContext _ctx;

        public DummyController(ContentContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }
        
        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}