using System.ComponentModel.DataAnnotations;

namespace MyAppAPI.Models.ReMap
{
    public class SearchDto
    {
        [MaxLength(100)]
        public string Search { get; set; }
    }
}