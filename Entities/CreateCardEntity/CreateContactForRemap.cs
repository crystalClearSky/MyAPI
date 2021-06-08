using System;

namespace Entities.CreateCardEntity
{
    public class CreateContactForRemap
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public DateTime TimeSent { get; set; }
    }
}