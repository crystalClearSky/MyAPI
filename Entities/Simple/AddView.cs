namespace Entities.Simple
{
    public class AddView
    {
        public int? ViewedByGuestId { get; set; }
        public int? ViewedByAvatarId { get; set; }
        public int? ViewedByUnregisteredGuest { get; set; }
        public int? AnonymousId { get; set; }
    }
}