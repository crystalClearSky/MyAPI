using System.ComponentModel.DataAnnotations;

namespace Entities.Simple
{
    public class GuestBasic
    {
        [Required]
        public string IPAddress { get; set; }
    }
}