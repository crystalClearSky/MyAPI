using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ContactEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(60)]
        public string FirstName { get; set; }
        [MaxLength(60)]
        public string LastName { get; set; }
        [MaxLength(150)]
        public string Subject { get; set; }
        [MaxLength(300)]
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public DateTime TimeSent { get; set; }
        //public bool IsApproved { get; set; }
    }
}