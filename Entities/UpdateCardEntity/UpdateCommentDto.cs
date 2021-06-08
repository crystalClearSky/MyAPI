using System;
using System.ComponentModel.DataAnnotations;

namespace MyAppAPI.Entities.UpdateCardEntity
{
    public class UpdateCommentDto
    {
        [MaxLength(180)]
        [MinLength(10)]
        public string Message { get; set; }
        public string FlaggedCommentMessages { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}