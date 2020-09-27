using System.ComponentModel.DataAnnotations;
namespace MyAppAPI.Models
{
    public class CommentCreation
    {
        [MaxLength(180)]
        [MinLength(10)]
        public string Message { get; set; }
    }
}