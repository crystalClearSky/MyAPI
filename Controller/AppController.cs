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

namespace MyAppAPI.Controller
{
    [ApiController]
    [Route("api/content")]
    public class AppController : ControllerBase
    {
        public IGalleryData GalleryDb { get; }
        public AppController(IGalleryData galleryDb)
        {
            this.GalleryDb = galleryDb;

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
            var results = GalleryData.Current.getAllCards();
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
        public IActionResult GetGalleryByTag([FromQuery] string[] words) // frombody list of tags
        {
            var tags = new List<Tag>();
            foreach (var word in words)
            {
                tags.Add(new Tag() { TagItem = word });
            }
            var cards = GalleryData.Current.GetGalleryCardsByTags(tags);
            if (cards.Count() <= 0)
            {
                return NotFound("No Cards found!");
            }

            return Ok(cards);
        }
        [HttpPost]
        public IActionResult CreateGalleryCard([FromBody] CreateGalleryCard createdGalleryCard)
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

            // Add Put for Udtating a gallery Card.
            // Add Patch to Ammend part of a Gallery Card.
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGalleryCard(int id, 
        [FromBody] UpdateGalleryCard updateGalleryCard)
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

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGalleryCard(int id)
        {
            GalleryData.Current.DeleteGalleryCardById(id);

            return NoContent();
        }
    }
}