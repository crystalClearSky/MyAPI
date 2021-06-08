using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyAppAPI.Context;
using MyAppAPI.Entities;
using MyAppAPI.Entities.ContractsForDbContext;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyAppAPI.ContentRepository
{
    public class AvatarDataRepo : IAvatarContext //Change to UserDataRepo handles both Avatar and Superuser
    {
        private readonly ContentContext _ctx;
        public AvatarDataRepo(ContentContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public bool AddNewAvatar(AvatarEntity avatar)
        {
            if (AddFruitToAvatar(avatar) != null)
            {
                if (!Exist(avatar.CurrentIP))
                {
                    _ctx.Add(avatar);

                    return Save();
                }
            }
            return false;
        }

        public bool DeleteAvatar(int id)
        {
            var avatar = _ctx.AvatarEntity
            .Include(c => c.Comments)
            .Include(l => l.Likes)
            .Include(v => v.UpVotes)
            .AsSingleQuery()
            .FirstOrDefault(a => a.Id == id);
            if (avatar != null)
            {
                _ctx.AvatarEntity.Remove(avatar);
                return Save();
            }
            return false;
        }



        public IEnumerable<AvatarEntity> GetAllAvatars(int option = 0)
        {
            // using (var context = new ContentContext())
            // {
            //     var avatar = context.AvatarEntity.Include(a =>a.Comments)
            //     .Include(c =>c.Replies)
            //     .Include(l => l.Likes)
            //     .Include(x =>x.TagEntities)
            //     .Include(v =>v.UpVotes).ToList();
            //     avatar.Select(c =>c.Comments).ToList();
            //     avatar.Select(a =>a.Replies).ToList();
            //     avatar.Select(c =>c.Comments.Select(r =>r.Response)).ToList();
            //     avatar.Select(t =>t.TagEntities).ToList();
            //     avatar.Select(v =>v.UpVotes).ToList();

            //     return context.AvatarEntity.ToList();
            // }
            var result = new List<AvatarEntity>();

            if (option == 1)
            {
                result = _ctx.AvatarEntity.ToList();
            }
            else
            {
                result = _ctx.AvatarEntity
               .Include(c => c.Comments)
               .Include(r => r.Replies)
               .Include(t => t.TagEntities)
               .Include(v => v.UpVotes)
               .Include(l => l.Likes)
               .Include(vi => vi.ViewedContent)
               .AsSingleQuery()
               .ToList();
            }

            return result;

        }

        public AvatarEntity GetAvatarById(int id = 0, string ip = "", string name = "")
        {
            var avatar = new AvatarEntity();
            var result = GetAllAvatars();
            if (id > 0)
            {
                avatar = result.Where(a => a.Id == id).FirstOrDefault();
            }
            if (!string.IsNullOrWhiteSpace(ip))
            {
                avatar = result.Where(a => a.CurrentIP == ip).FirstOrDefault();
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                avatar = result.Where(a => a.Name == name).FirstOrDefault();
            }

            return avatar;
        }

        public IEnumerable<CommentEntity> GetCommentsByAvatar(int id = 0, bool admin = false) //
        {
            var avatar = new AvatarEntity();
            var comments = new List<CommentEntity>();
            if (admin)
            {
                comments = _ctx.CommentEntity
                .Include(c => c.ReplyTo)
                .Include(l => l.Likes)
                .Include(f => f.Flags)
                .Where(s => s.IsSuperUser == admin)
                .AsSingleQuery()
                .ToList();
            }
            else
            {
                var result = GetAllAvatars();
                avatar = result.Where(a => a.Id == id).FirstOrDefault();
                comments = avatar.Comments;
            }

            return comments;
        }

        public async Task<IEnumerable<CommentEntity>> GetAllComment()
        {
            var comments = await _ctx.CommentEntity
            .Include(l => l.Likes)
            .Include(r => r.RepliesTo)
            .Include(f => f.Flags)
            .Include(li => li.Links)
            .AsSingleQuery().ToListAsync();

            return comments.ToList();
        }

        public int GetTotalCommentCountCount(int avatarId = 0, int contentId = 0) {
            if(avatarId > 0)
                return _ctx.CommentEntity.Where(a => a.AvatarId == avatarId).Count();
            
            if(contentId > 0)
                return _ctx.CommentEntity.Where(c => c.CardEntityId == contentId).Count();

            return _ctx.CommentEntity.Count();
        }

        public IEnumerable<CommentEntity> GetAllCommmentsByCard(int contentId, int pageSize, int pageNumber) 
        {
            var skipCount = (pageNumber - 1) * pageSize;
            var result = _ctx.CommentEntity.Where(x => x.CardEntityId == contentId)
            .Include(l => l.Likes)
            .Include(r => r.ReplyTo).ThenInclude(l => l.Flags)
            .Include(f => f.Flags)
            .Include(li => li.Links)
            .OrderByDescending(x => x.Id).Skip(skipCount).Take(pageSize).AsSingleQuery().ToList();

            return result;
        }

        public CommentEntity GetCommentById(int id)
        {
            var comments = GetAllComment().Result;
            return comments.Where(c => c.Id == id).FirstOrDefault();

        }

        public bool AddCommentByUser(CommentEntity comment, int userId = 0, bool? admin = null, int commentIdToAddOn = 0) //
        {
            var reply = new ReplyEntity();
            var resultForComment = AddJustComment(comment);
            if (comment.IsReply && resultForComment)
            {
                reply.AvatarId = userId;
                reply.CommentId = _ctx.CommentEntity.Max(c => c.Id);
                reply.ResponseToCommentId = commentIdToAddOn;
                reply.IsSuperUser = admin;

                _ctx.ReplyEntity.Add(reply);
                bool result = Save();

                var check = _ctx.ReplyEntity.Include(r => r.ResponseComment).AsSingleQuery().ToList();

                return result;
            }
            return resultForComment;
        }

        public bool AddOrRemoveFlag(FlagEntity flag, int userId = 0)
        {
            if (userId == 0)
            {
                using (var context = new ContentContext())
                {
                    context.Add(flag);
                    return context.SaveChanges() > 0;

                }
                // _ctx.Add(flag);
            }

            if (userId > 0)
            {
                using (var context = new ContentContext())
                {
                    context.Remove(flag);
                    return context.SaveChanges() > 0;

                }
                // _ctx.Remove(flag);
            }
            return Save();
        }

        public bool AddJustComment(CommentEntity comment)
        {
            _ctx.CommentEntity.Add(comment);
            return _ctx.SaveChanges() > 0;
        }

        public bool AddOrRemoveLike(LikeEntity like = null, int userId = 0)
        {
            if (userId > 0)
            {
                _ctx.Remove(like);
            }
            if (userId == 0)
            {
                _ctx.Add(like);
            }

            return Save();
        }
        public bool Save() => _ctx.SaveChanges() > 0;
        public bool Exist(string ip = "", int id = 0)
        {
            var result = GetAllAvatars();
            return result.Any(a => a.CurrentIP == ip || a.Id == id);
        }
        public bool CheckIn(int id = 0, string ip = "")
        {
            var avatar = new AvatarEntity();
            if (id > 0) avatar = GetAvatarById(id);

            if (!string.IsNullOrWhiteSpace(ip)) avatar = GetAvatarById(ip: ip);
            avatar.LastCheckedIn = DateTime.Now;
            avatar.IsCheckedIn = true;
            _ctx.Attach(avatar);
            _ctx.Entry(avatar).State = EntityState.Modified;

            return Save();
        }

        public string LogOutUser(int userId)
        {
            string message = string.Empty;
            var avatar = GetAvatarById(userId);
            avatar.IsCheckedIn = false;
            _ctx.AvatarEntity.Attach(avatar);
            _ctx.Entry(avatar).State = EntityState.Modified;
            if (Save())
            {
                message = $"User {avatar.Name} has successfully logged out.";
            }
            return message;
        }
        public bool UpdateContent(CommentEntity comment)
        {
            _ctx.Attach(comment);
            _ctx.Entry(comment).State = EntityState.Modified;
            return Save();
        }
        public bool DeleteCommentByAvatar(int userId = 0, bool? admin = null, int commentId = 0)
        {
            var comment = new CommentEntity();
            if (admin == true)
            {
                var comments = _ctx.CommentEntity
                .Include(c => c.ReplyTo).AsTracking()
                .Include(l => l.Likes)
                .Include(f => f.Flags)
                .AsSingleQuery()
                .ToList(); // to allow deletion of any user's post
                comment = comments.FirstOrDefault(c => c.Id == commentId);
            }
            else
            {
                var avatars = _ctx.AvatarEntity
                .Include(c => c.Comments)
                .ThenInclude(r => r.ReplyTo).AsTracking()
                .AsSingleQuery()
                .ToList();

                comment = avatars
                .FirstOrDefault(c => c.Id == userId).Comments
                .FirstOrDefault(r => r.Id == commentId);
            }
            if (comment != null)
            {
                _ctx.Remove(comment);
                return Save();
            }
            return false;
        }

        public bool UpdateAvatar(AvatarEntity avatar)
        {
            _ctx.AvatarEntity.Attach(avatar);
            _ctx.Entry(avatar).State = EntityState.Modified;
            return Save();
        }

        public IEnumerable<AvatarEntity> GetUserSearchResult(List<string> queries, int limit)
        {
            var finalResult = new List<AvatarEntity>();
            var avatars = GetAllAvatars();
            foreach (var word in queries)
            {
                foreach (var avatar in avatars)
                {

                    if (avatar.Name.ToLower().Contains(word.ToLower()))
                    {
                        //card.SearchIndexValue++;
                        if (avatar.Name.ToLower().Contains(word.ToLower()) && !finalResult.Contains(avatar))
                        {
                            finalResult.Add(avatar);
                        }
                    }
                }
            }
            return finalResult.Take(limit);
        }

        public AvatarEntity AddFruitToAvatar(AvatarEntity avatar)
        {

            var profileItems = _ctx.FruitItemsEntity.ToList();

            if (profileItems != null)
            {
                var profileItem = profileItems.Where(p => p.Id == profileItems.Max(c => c.Id)).FirstOrDefault();
                avatar.Name = profileItem.FruitName;
                avatar.AvatarImgUrl = profileItem.FruitImg;

                _ctx.FruitItemsEntity.Remove(profileItem);
                if (Save() && !(string.IsNullOrWhiteSpace(avatar.Name)))
                {
                    return avatar;
                }
            }
            // add count for remaining tokens
            return null;

        }

        public IEnumerable<FruitItemsEntity> GetRemainingFruit() {
            var remainingFruit = _ctx.FruitItemsEntity.ToList();
            return remainingFruit;
        }

        public List<object> DetectObjectType(string toDetect, int contentId, int avatarId)
        {
            char[] charsToTrim = { '*', ' ', '@', '#', '/' };
            var result = new List<object>();
            var user = new AvatarEntity();
            var tag = new TagEntity();
            if (toDetect.Contains('@'))
            {
                var regex = new Regex(@"\s@([\w$]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                foreach (Match nameMatch in regex.Matches(toDetect))
                {
                    string item = nameMatch.Groups[0].Value;
                    if (item.Contains('@'))
                    {
                        string nameResult = item.Trim(charsToTrim);
                        user = GetAvatarById(name: nameResult);
                        result.Add(user);
                    }
                }

            }
            if (toDetect.Contains('#'))
            {
                var card = _ctx.CardEntity.Find(contentId);
                var regex2 = new Regex(@"\s#([\w$]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                foreach (Match nameMatch in regex2.Matches(toDetect))
                {
                    string item = nameMatch.Groups[0].Value;
                    if (item.Contains('#'))
                    {
                        var tags = _ctx.CardEntity.Where(c => c.Id == contentId).SelectMany(t => t.Tags).ToList();
                        string tagResult = item.Trim(charsToTrim);
                        if (!tags.Any(t => t.TagItem == tagResult))
                        {
                            tag = new TagEntity()
                            {
                                Id = 0,
                                TagItem = tagResult,
                                ByAvatarId = avatarId,
                            };
                            card.Tags.Add(tag);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(tag.TagItem))
                {
                    _ctx.CardEntity.Attach(card);
                    _ctx.Entry(card).State = EntityState.Modified;
                    if (_ctx.SaveChanges() > 0)
                    {
                        result.Add("Done!");
                    }
                }
            }

            return result;
        }

    }
}



