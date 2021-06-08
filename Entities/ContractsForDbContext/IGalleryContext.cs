using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using MyAppAPI.Entities;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;

namespace MyAppAPI.Entities.ContractsForDbContext
{
    public interface IGalleryContext
    {
        bool EditOption(EnableOptionsEntity editOption);
        EnableOptionsEntity GetOption(string ip = "", int id = 0, DateTime? seen = null);
        bool AddOption(EnableOptionsEntity addOption);
        bool AddView(ViewEntity addView);
        bool UpdateView(ViewEntity updateView);
        IEnumerable<CardEntity> GetAllCards(int pageSize = 4, int pageNumber = 1, int postType = -1, List<CardEntity> allCards = null);
        CardEntity GetCardById(int id);
        IEnumerable<CardEntity> GetGalleryCardsByTags(List<TagEntity> tags);
        IEnumerable<CommentEntity> GetCommentWithTheseSearchTerms(List<string> queries);
        IEnumerable<CardEntity> SearchForCard(List<string> queries, int limit = 3, int postType = -1, int skipBy = 1, int avatarId = -1, string typeSearch = "");
        bool AddGalleryCard(CardEntity galleryCard);
        bool AddOrRemoveUpVote(VoteEntity Upvote, int userId = 0);
        
        bool UpdateGalleryCard(CardEntity galleryCard);
        bool CardExists(int id);
        List<TagEntity> GetAllTags(string tagQuery = "", int pageLimit = 4);
        bool AddImagesToCard(List<ImageEntity> images);

        bool DeleteGalleryCardById(int id);
        bool Save();
    }
}