namespace Entities.ContractsForDbContext
{
    public interface IContactContext
    {
        bool SendEmail(ContactEntity message);
        ContactEntity GetEmail(ContactEntity contact);
        bool Save();
    }
}