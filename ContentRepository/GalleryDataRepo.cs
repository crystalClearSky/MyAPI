using System.Reflection.Metadata;
using System.Net.Mail;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MyAppAPI.Context;
using MyAppAPI.Entities.ContractsForDbContext;
using MyAppAPI.Entities;
using System.Threading.Tasks;
using AutoMapper;
using MyAppAPI.ReMap;
using Entities;

namespace MyAppAPI.ContentRepository
{
    public class GalleryDataRepo : IGalleryContext
    {
        private readonly ContentContext _ctx;
        private readonly IMapper _mapper;

        public GalleryDataRepo(ContentContext ctx, IMapper mapper)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool AddGalleryCard(CardEntity galleryCard)
        {
            if (galleryCard != null)
            {
                _ctx.CardEntity.Add(galleryCard);
                return Save();
            }

            return false;
        }

        public bool DeleteGalleryCardById(int id)
        {
            var card = GetCardById(id);
            if (card != null)
            {
                _ctx.CardEntity.Remove(card);

                return Save();
            }
            return false;
        }

        public IEnumerable<CardEntity> GetAllCards(int pageSize = 4, int pageNumber = 1, int postType = -1, List<CardEntity> allCards = null)
        {
            var cards = new List<CardEntity>();
            if (allCards == null)
            {
                cards = GetEveryCard().ToList();
            }
            else
            {
                cards = allCards;
            }
            // .Include(c => c.Comments.OrderByDescending(x => x.Id)).ThenInclude(l => l.Likes).Include(r => r.Comments).ThenInclude(j => j.ReplyTo).Include(r => r.Comments).ThenInclude(f => f.Flags)
            // .Include(l => l.UpVotes)
            // .Include(i => i.Images)
            // .Include(v => v.Views)
            // .Include(t => t.Tags).AsTracking()
            // .AsSingleQuery().OrderByDescending(x => x.Id).ToList();
            var result = new List<CardEntity>();
            var skipCount = (pageNumber - 1) * pageSize;
            if (postType > -1)
            {
                result = cards.Where(p => p.PostType == (PostType)postType).ToList();
                // result.Count() > pageSize ? (pageNumber - 1) * pageSize : 0
                cards = result.Skip(skipCount).Take(pageSize).OrderBy(x => x.Id).ToList();
            }
            else
            {
                cards = cards.Skip(skipCount).Take(pageSize).OrderBy(x => x.Id).ToList();
            }
            // var finalCards = new List<CardEntity>();
            // foreach (var card in cards)
            // {
            //     var finalResult = AppendLinkInCommentMessage(card);
            //     finalCards.Add(finalResult);
            // }
            // var cards = _ctx.CardEntity
            // .Include(c => c.Comments).ThenInclude(r => r.Response).ThenInclude(i => i.ResponseComment)
            // .Include(l => l.UpVotes)
            // .Include(f => f.Flags)
            // .Include(t => t.Tags).AsTracking().ToList();

            // var results = new List<CardEntity>();
            // foreach (var card in cards)
            // {
            //     results.Add(FormatComments(card));
            // }

            return cards;
        }

        private IEnumerable<CardEntity> GetEveryCard(int commentPageSize = 2, int commentPageNumber = 1)
        {
            var skipCount = (commentPageNumber - 1) * commentPageSize;
            var cards = _ctx.CardEntity
            .Include(c => c.Comments.OrderByDescending(x => x.Id).Skip(skipCount).Take(commentPageSize)).ThenInclude(l => l.Likes).Include(r => r.Comments).ThenInclude(j => j.ReplyTo).Include(r => r.Comments).ThenInclude(f => f.Flags)
            .Include(l => l.UpVotes)
            .Include(i => i.Images)
            .Include(v => v.Views)
            .Include(t => t.Tags).AsTracking()
            .AsSingleQuery().OrderByDescending(x => x.Id).ToList();

            var finalCards = new List<CardEntity>();
            foreach (var card in cards)
            {
                var finalResult = AppendLinkInCommentMessage(card);
                finalCards.Add(finalResult);
            }

            return finalCards;
        }

        public CardEntity GetCardById(int id)
        {
            // using (var context = new ContentContext())
            // {

            //     var content = await context.CardEntity.Include(t => t.Tags).ToListAsync();
            //     context.CardEntity.Include(f => f.Flags)
            //     .Include(c => c.Comments);

            //     content.Select(c => c.Comments.Where(r => r.IsReply == false)).ToList();
            //     content.Select(t => t.Tags).ToList();
            //     content.Select(f => f.Flags).ToList();
            //     content.Select(l => l.UpVotes).ToList();
            //     content.Select(c => c.Comments.Select(t => t.Response.Where(x => x.Reply.IsReply))).ToList();
            //     content.Select(c => c.Comments.Select(t => t.Response.Select(x => x.Reply))).ToList();
            //     content.Select(c => c.Comments.Select(r => r.Likes)).ToList();

            //     return FormatComments(context.CardEntity.Find(id));
            // }

            var card = _ctx.CardEntity
            .Include(c => c.Comments).ThenInclude(r => r.ReplyTo)
            .Include(r => r.Comments).ThenInclude(f => f.Flags)
            .Include(l => l.UpVotes)
            .Include(i => i.Images)
            .Include(v => v.Views)
            .Include(t => t.Tags).AsTracking()
            .AsSingleQuery()
            .FirstOrDefault(c => c.Id == id);

            var result = FormatComments(card);


            return result;
        }

        public IEnumerable<CardEntity> GetGalleryCardsByTags(List<TagEntity> tags)
        {
            var cards = GetAllCards();
            var finalResult = new List<CardEntity>();
            foreach (var tag in tags)
            {
                foreach (var card in cards)
                {
                    var tagItems = card.Tags.Select(t => t.TagItem).ToList();
                    foreach (var word in tagItems)
                    {
                        if (word.ToLower() == tag.TagItem.ToLower())
                        {
                            card.SearchIndexValue++;
                            if (word.ToLower() == tag.TagItem.ToLower() && !finalResult.Contains(card))
                            {
                                finalResult.Add(card);
                            }
                        }
                    }

                }
            }
            var result = finalResult.OrderByDescending(c => c.SearchIndexValue);

            return result;
        }

        public bool AddOption(EnableOptionsEntity addOption)
        {
            _ctx.EnableOptionsEntity.Add(addOption);
            return Save();
        }

        public EnableOptionsEntity GetOption(string ip = "", int id = 0, DateTime? seen = null)
        {
            var option = new EnableOptionsEntity();
            if (!string.IsNullOrWhiteSpace(ip))
            {
                option = _ctx.EnableOptionsEntity.FirstOrDefault(o => o.IpAddress == ip);
            }
            if (id > 0)
            {
                option = _ctx.EnableOptionsEntity.FirstOrDefault(o => o.Id == id);
            }
            if (seen != null)
            {
                option = _ctx.EnableOptionsEntity.FirstOrDefault(o => o.FirstSeen == seen);
            }
            if (option == null || option.Id == 0)
            {
                return null;
            }
            else
            {
                return option;
            }

        }

        public bool EditOption(EnableOptionsEntity editOption)
        {
            _ctx.EnableOptionsEntity.Attach(editOption);
            _ctx.Entry(editOption).State = EntityState.Modified;
            return Save();
        }

        public CardEntity FormatComments(CardEntity card)
        {
            var result = new List<CommentEntity>();
            if (card != null)
            {
                foreach (var comment in card.Comments.OrderByDescending(c => c.Id))
                {
                    if (!comment.IsReply)
                    {
                        result.Add(comment);
                    }
                }
                card.Comments = result;
                return card;
            }
            return card = null;
        }

        public bool UpdateGalleryCard(CardEntity galleryCard)
        {
            _ctx.Attach(galleryCard);
            _ctx.Entry(galleryCard).State = EntityState.Modified;
            return Save();

        }

        public bool CardExists(int id)
        {
            bool Exist = true;
            var card = _ctx.CardEntity.Find(id);
            if (card == null)
            {
                return !Exist;
            }
            return Exist;
        }

        public bool Save() => _ctx.SaveChanges() > 0;

        public IEnumerable<CardEntity> SearchForCard(List<string> queries, int limit = 3, int postType = -1, int skipBy = 1, int avatarId = -1, string typeSearch = "")
        {
            // switch statement for wide search or (concrete search (cards.contain(card))
            var cardIds = new List<int>();
            if (!string.IsNullOrEmpty(typeSearch) && avatarId > 0)
            {
                if (typeSearch == "comments")
                {
                    var comments = _ctx.CommentEntity.Where(a => a.AvatarId == avatarId).ToList();

                    for (int i = 0; i < comments.Count(); i++)
                    {
                        if (!cardIds.Contains(comments[i].CardEntityId))
                            cardIds.Add(comments[i].CardEntityId);
                    }
                }
                if (typeSearch == "upVotes")
                {
                    var upVotes = _ctx.VoteEntity.Where(a => a.VoteById == avatarId).ToList();

                    for (int i = 0; i < upVotes.Count(); i++)
                    {
                        if (!cardIds.Contains(upVotes[i].CardId))
                            cardIds.Add(upVotes[i].CardId);
                    }
                }
                if (typeSearch == "tags")
                {
                    var tags = _ctx.TagEntity.Where(a => a.ByAvatarId == avatarId).ToList();

                    for (int i = 0; i < tags.Count(); i++)
                    {
                        if (!cardIds.Contains(tags[i].CardId))
                            cardIds.Add(tags[i].CardId);
                    }
                }
                if (typeSearch == "likes")
                {
                    var likes = _ctx.LikeEntity.Where(a => a.LikedById == avatarId).ToList();
                    for (int i = 0; i < likes.Count(); i++)
                    {
                        if (!cardIds.Contains(_ctx.CommentEntity.Where(c => c.Id == likes[i].CommentId).FirstOrDefault().CardEntityId))
                        {
                            cardIds.Add(_ctx.CommentEntity.Where(c => c.Id == likes[i].CommentId).FirstOrDefault().CardEntityId);
                        }
                    }
                }
                if(typeSearch == "views")
                {
                    var views = _ctx.ViewEntity.Where(a => a.ViewedByAvatarId == avatarId).ToList();

                    for (int i = 0; i < views.Count(); i++)
                    {
                        if (!cardIds.Contains((int)views[i].CardEntityId))
                            cardIds.Add((int)views[i].CardEntityId);
                    }
                }
            }
            var cards = new List<CardEntity>();
            var finalResult = new List<CardEntity>();
            if (!string.IsNullOrEmpty(typeSearch))
            {
                for (var i = 0; i < cardIds.Count(); i++)
                {
                    finalResult.Add(GetCardById(cardIds[i]));
                }
            }
            else
            {
                cards = GetEveryCard().ToList();
            }

            
            if (queries.Count() > 0)
            {
                foreach (var query in queries)
                {
                    foreach (var card in cards)
                    {
                        var text = string.Join(" ", card.Comments.Select(a => a.Message).ToList());
                        text += (string.Join(" ", card.Tags.Select(t => t.TagItem).ToList()));
                        text += " " + card.Description;
                        text += " " + card.Title;
    
                        if (text.ToLower().Contains(query))
                        {
                            card.SearchIndexValue++;
                            if (text.ToLower().Contains(query) && !finalResult.Contains(card))
                            {
                                finalResult.Add(card);
                            }
                        }
                    }
                }
            }
            var lastResult = GetAllCards(allCards: finalResult, pageNumber: skipBy, postType: postType);
            var result = lastResult.OrderByDescending(c => c.SearchIndexValue).Take(limit);

            return result;
        }
        public bool AddImagesToCard(List<ImageEntity> images)
        {
            _ctx.ImageEntity.AddRange(images);
            return Save();
        }
        public IEnumerable<CommentEntity> GetCommentWithTheseSearchTerms(List<string> queries)
        {
            var comments = _ctx.CommentEntity
            .Include(l => l.Likes)
            .Include(li => li.Links)
            .AsSingleQuery()
            .ToList();
            var finalResult = new List<CommentEntity>();


            if (queries != null)
            {
                foreach (var query in queries)
                {
                    foreach (var comment in comments)
                    {
                        var text = comment.Message;

                        if (text.ToLower().Contains(query))
                        {
                            comment.SearchIndexValue++;
                            if (text.ToLower().Contains(query) && !finalResult.Contains(comment))
                            {
                                finalResult.Add(comment);
                            }
                        }
                    }
                }
                //finalResult = FormatCommentReplies(finalResult);
                var result = finalResult.OrderByDescending(c => c.SearchIndexValue);
                return result;
            }
            return comments;

        }

        public bool AddOrRemoveUpVote(VoteEntity Upvote, int userId = 0)
        {
            if (userId > 0)
            {
                using (var context = new ContentContext())
                {
                    context.Remove(Upvote);
                    return context.SaveChanges() > 0;

                }
                //_ctx.Remove<VoteEntity>(Upvote);
            }

            if (userId == 0)
            {
                using (var context = new ContentContext())
                {
                    context.Add(Upvote);
                    return context.SaveChanges() > 0;

                }
                //_ctx.Add<VoteEntity>(Upvote);
            }

            return Save();
        }



        public bool AddView(ViewEntity addView)
        {
            using (var context = new ContentContext())
            {
                context.ViewEntity.Add(addView);
                return context.SaveChanges() > 0;
            }

        }

        public bool UpdateView(ViewEntity updateView)
        {
            using (var context = new ContentContext())
            {
                context.ViewEntity.Attach(updateView);
                context.Entry(updateView).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }

        public List<TagEntity> GetAllTags(string tagQuery = "", int pageLimit = 4)
        {
            var tagResults = new List<TagEntity>();
            var allTags = _ctx.CardEntity.SelectMany(t => t.Tags).ToList();
            foreach (var tag in allTags)
            {
                var result = _ctx.CardEntity.Where(c => c.Tags.Any(t => t.TagItem == tag.TagItem)).ToList();

                tag.CardsWithThisId = result.Count();
                tagResults.Add(tag);
            }
            var finalResult = NarrowTagResult(tagResults).Where(t =>(tagQuery == "" || t.TagItem.Contains(tagQuery))).OrderByDescending(x => x.CardsWithThisId).Take(pageLimit).ToList();

            return finalResult;
        }

        public List<TagEntity> NarrowTagResult(List<TagEntity> tags)
        {
            var tagResults = new List<TagEntity>();
            var allTags = _ctx.CardEntity.SelectMany(t => t.Tags).ToList();
            foreach (var tag in tags)
            {
                if (!tagResults.Any(t => t.TagItem == tag.TagItem))
                {
                    tagResults.Add(tag);
                }
            }

            return tagResults;

        }


        public List<CommentEntity> FormatCommentReplies(List<CommentEntity> comments)
        {
            var results = new List<CommentEntity>();
            foreach (var comment in comments)
            {
                if (!comment.IsReply)
                {

                }
            }
            return null;
        }
        private CardEntity AppendLinkInCommentMessage(CardEntity card)
        {
            var comments = card.Comments;
            var finalComments = new List<CommentEntity>();
            foreach (var comment in comments)
            {

                if (comment.Message.Contains("&"))
                {
                    string[] words = comment.Message.Split(" ");
                    var finalMessage = "";
                    foreach (var word in words)
                    {
                        if (word.Contains('&'))
                        {
                            finalMessage += GetLinkObject(word.Trim('&')) + " ";
                        }
                        else
                        {
                            finalMessage += word + " ";
                        }
                    }
                    comment.Message = finalMessage.Trim();
                    finalComments.Add(comment);
                }
                else
                {
                    finalComments.Add(comment);
                }
            }
            card.Comments = finalComments;
            return card;
        }
        private string GetLinkObject(string contentId)
        {
            return _ctx.LinkEntity.FirstOrDefault(r => r.LinkIndex == contentId).Link;

        }

    }
}

