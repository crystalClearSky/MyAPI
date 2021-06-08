namespace MyAppAPI.Entities.Simple
{
    public class AddUpVote
    {
        public int? VoteById { get; set; }
        public int? VoteByGuest { get; set; }
        public int CardId { get; set; }
    }
}