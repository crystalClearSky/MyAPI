using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace MyAppAPI.Entities.ContractsForDbContext
{
    public interface IAvatarContext
    {
        bool AddOrRemoveFlag(FlagEntity flag, int userId = 0);
        AvatarEntity GetAvatarById(int id = 0, string ip = "", string name = "");
        IEnumerable<AvatarEntity> GetAllAvatars(int option = 0);
        IEnumerable<CommentEntity> GetCommentsByAvatar(int id = 0, bool admin = false);
        bool AddNewAvatar(AvatarEntity avatar);
        IEnumerable<AvatarEntity> GetUserSearchResult(List<string> queries, int limit);
        bool AddCommentByUser(CommentEntity comment, int userId = 0, bool? admin = null, int commentIdToAddOn = 0);
        bool DeleteCommentByAvatar(int userId = 0, bool? admin = null, int commentId = 0);
        bool Exist(string ip = "", int id = 0);
        bool UpdateContent(CommentEntity comment);
        Task<IEnumerable<CommentEntity>> GetAllComment();
        int GetTotalCommentCountCount(int avatarId = 0, int contentId = 0);
        bool AddOrRemoveLike(LikeEntity like = null, int userId = 0);
        CommentEntity GetCommentById(int id);
        string LogOutUser(int userId);
        bool UpdateAvatar(AvatarEntity avatar);
        IEnumerable<FruitItemsEntity> GetRemainingFruit();
        bool CheckIn(int id = 0, string ip = "");
        bool DeleteAvatar(int id);
        IEnumerable<CommentEntity> GetAllCommmentsByCard(int contentId, int pageSize, int pageNumber);
        List<object> DetectObjectType(string toDetect, int contentId, int avatarId);
        bool Save();
    }
}