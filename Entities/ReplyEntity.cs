using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyAppAPI.Entities
{
    public class ReplyEntity
    {
        public int? AvatarId { get; set; }
        [ForeignKey("CommentId")]
        public CommentEntity Reply { get; set; }
        public int CommentId { get; set; }
      //  public ReplyOnEntity ReplyOn { get; set; }
        // public int ReplyOnId { get; set; }
        // [ForeignKey("ReplyOnId")]
        // public ReplyOnEntity ReplyOn { get; set; }
        // [ForeignKey("UserId")]
        // public UserEntity UserEntity { get; set; }
        // public int? UserId { get; set; }
        [ForeignKey("ResponseToCommentId")]
        public CommentEntity ResponseComment { get; set; }
        public int? ResponseToCommentId { get; set; }

        public bool? IsSuperUser { get; set; }
        [ForeignKey("IsSuperUser")]
        public UserEntity UserEntity { get; set; }
        
    }
}