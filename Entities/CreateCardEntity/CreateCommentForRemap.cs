using System;
using System.ComponentModel.DataAnnotations;

namespace MyAppAPI.Entities.CreateCardEntity
{
    public class CreateCommentForRemap
    {
        public int? AvatarId { get; set; }
        [MaxLength(200)]
        public string Message { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int? ResponseToAvatarId { get; set; }

        [Required]
        public int CardEntityId { get; set; }
        public bool IsReply { get; set; }
        public bool? IsSuperUser { get; set; }
        public int? ReplyToCommentId { get; set; }
        public string FlaggedCommentMessages { get; set; }

    }
}