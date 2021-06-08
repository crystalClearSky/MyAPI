using System.ComponentModel.DataAnnotations;

namespace Entities.Simple
{
    public class FlagBasic
    {
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string ReasonText { get; set; }
    }
}