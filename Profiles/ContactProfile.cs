using AutoMapper;

namespace Profiles
{
    public class ContactProfile: Profile
    {
        public ContactProfile()
        {
            CreateMap<Entities.ContactEntity, Entities.CreateCardEntity.CreateContactForRemap>().ReverseMap();
        }
    }
}