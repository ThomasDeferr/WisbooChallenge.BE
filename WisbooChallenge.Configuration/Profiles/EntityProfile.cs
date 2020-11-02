using AutoMapper;
using WisbooChallenge.Entities.Classes;
using WisbooChallenge.Helpers.Resources.Inputs;

namespace WisbooChallenge.Configuration.Profiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<EntityModelInput, VideoMedia>();
        }
    }
}