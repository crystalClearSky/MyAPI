using System.Collections.Generic;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;

namespace MyAppAPI.Services
{
    public interface IGalleryData
    {
         IEnumerable<GalleryCard> getAllCards();
         GalleryCard getCardById(int id);
         IEnumerable<GalleryCard> GetGalleryCardsByTags(List<Tag> tags);
         void AddGalleryCard(GalleryCard galleryCard);

         void DeleteGalleryCardById(int id);
        //  List<Comment> GetComments(int id);
    }
}