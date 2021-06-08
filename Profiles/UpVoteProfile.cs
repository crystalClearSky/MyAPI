using AutoMapper;

namespace MyAppAPI.Profiles
{
    public class UpVoteProfile : Profile
    {
        public UpVoteProfile()
        {
            CreateMap<Entities.VoteEntity, Entities.Simple.AddUpVote>().ReverseMap();
        }
    }
}