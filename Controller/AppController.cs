using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.AppRepository;
using MyAppAPI.Models.GalleryModel;
using MyAppAPI.Services;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel.CreateModel;
using MyAppAPI.Models.GalleryModel.UpdateModel;
using Microsoft.Extensions.Logging;

namespace MyAppAPI.Controller
{
    [ApiController]
    [Route("api/content")]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;

        public IGalleryData GalleryDb { get; }
        public AppController(IGalleryData galleryDb, ILogger<AppController> logger)
        {

            this.GalleryDb = galleryDb;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public IActionResult GetContents()
        {
            // return new JsonResult(
            //     new List<object>()
            //     {
            //         GalleryData.Current.Cards
            //     }
            // );
            IEnumerable<GalleryCard> results = new List<GalleryCard>();
            try
            {
                results = GalleryData.Current.getAllCards();
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to retrieve gallery cards data.", ex);
                return StatusCode(500, "There was an expection found.");
            }
            return Ok(results);
        }

        [HttpGet("{id}", Name = "GetGallery")]
        public IActionResult GetContent(int id)
        {
            var card = new GalleryCard();
            card = GalleryData.Current.getCardById(id);

            if (card == null)
            {
                string message = $"The data with id {id} was not found!";
                return NotFound(message);
            }

            return Ok(card);
        }
        [HttpGet("tag/")]
        public IActionResult GetGalleryByTag([FromQuery] string q) // frombody list of tags
        {
            try
            {
              var tags = new List<Tag>();
              
              if (!string.IsNullOrWhiteSpace(q))
              {
                  var result = q.Split(' ');
                  foreach (var word in result)
                  {
                      tags.Add(new Tag() { TagItem = word });
                  }
              }
              else
              {
                  return BadRequest("Empty request!");
              }
              
  
              var cards = GalleryData.Current.GetGalleryCardsByTags(tags);
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
        [HttpPost]
        public IActionResult CreateGalleryCard([FromBody] CreateGalleryCard createdGalleryCard)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var maxGalleryCardId = GalleryDb.getAllCards().Max(g => g.Id);

                var finalGalleryCard = new GalleryCard()
                {
                    Id = ++maxGalleryCardId,
                    Title = createdGalleryCard.Title,
                    ImageUrl = createdGalleryCard.ImageUrl,
                    Descritpion = createdGalleryCard.Descritpion,
                    Tags = createdGalleryCard.Tags,
                    CreatedOn = DateTime.UtcNow
                };
                GalleryData.Current.Cards.Add(finalGalleryCard);
                return CreatedAtRoute(
                    "GetGallery",
                    new { id = finalGalleryCard.Id },
                    finalGalleryCard
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to CREATE a gallery card.", ex);
                return StatusCode(500, "There was an expection at CREATE Gallery Controller.");
            }

            // Add Put for Udtating a gallery Card.
            // Add Patch to Ammend part of a Gallery Card.
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGalleryCard(int id,
        [FromBody] UpdateGalleryCard updateGalleryCard)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var galleryCardStore = GalleryData.Current.Cards.FirstOrDefault(g => g.Id == id);
                if (galleryCardStore == null)
                {
                    return NotFound();
                }

                galleryCardStore.Title = updateGalleryCard.Title;
                if (updateGalleryCard.ImageUrl != null)
                {
                    galleryCardStore.ImageUrl = updateGalleryCard.ImageUrl;
                }
                if (updateGalleryCard.Descritpion != null)
                {
                    galleryCardStore.Descritpion = updateGalleryCard.Descritpion;
                }
                if (updateGalleryCard.Tags != null)
                {
                    galleryCardStore.Tags = updateGalleryCard.Tags;
                }

                return Ok($"Gallery Card created: {galleryCardStore.Title}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to UPDATE a gallery card with ID: {id}.", ex);
                return StatusCode(500, "There was an expection at UPDATE Gallery Controller");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGalleryCard(int id)
        {
            try
            {
                var galleryCard = GalleryData.Current.getCardById(id);

                if (galleryCard != null)
                {
                    GalleryData.Current.DeleteGalleryCardById(id);
                    return Ok($"Gallery Card with Id: {id} has been deleted.");
                }
                return BadRequest($"The Gallery Card with ID: {id} does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to DELETE ID: {id} gallery card.", ex);
                return StatusCode(500, "There was an expection at DELETE Gallery Controller");
            }
        }
    }
}