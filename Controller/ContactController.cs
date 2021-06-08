using System;
using System.Net;
using System.Net.Mail;
using AutoMapper;
using ContentRepository;
using Entities;
using Entities.ContractsForDbContext;
using Entities.CreateCardEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.ReMap.CreateModelDto;

namespace Controller
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IContactContext _ctx;
        private readonly ILogger<ContactController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public ContactController(IContactContext ctx, ILogger<ContactController> logger, IMapper mapper, IConfiguration config)
        {
            _config = config;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        [HttpGet]
        public IActionResult GetAction()
        {
            return Ok("This is just a test message");
        }

        [HttpPost]
        public IActionResult SendAnEmail(CreateContactForRemap createdMessage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Input");
                }
                var official = new OfficialDto();
                _config.GetSection(official.Email).Bind(official);
                var fromAddress = new MailAddress(official.Address, "A Client Message");
                var toAddress = new MailAddress(official.Address, "To Me From Client");
                string fromPassword = official.Key;
                string subject = createdMessage.Subject + " - " + createdMessage.FirstName + " " + createdMessage.LastName;
                string body = createdMessage.Message;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                createdMessage.TimeSent = DateTime.Now;
                createdMessage.IsSent = true;
                if (_ctx.SendEmail(_mapper.Map<ContactEntity>(createdMessage)))
                {
                    return Ok("message has been sent");
                }
                return BadRequest("No thing was sent");
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error found at SENDEMAIL.", ex);
                return StatusCode(500, "There was an error found at SENDEMAIL.");
            }
        }
    }
}