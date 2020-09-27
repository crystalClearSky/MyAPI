using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System;

namespace MyAppAPI.Models
{
    public class CommentUpdate
    {
        [MaxLength(180)]
        [MinLength(10)]
        public string Message { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}