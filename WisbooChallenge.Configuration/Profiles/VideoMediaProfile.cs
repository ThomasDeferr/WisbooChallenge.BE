using AutoMapper;
using WisbooChallenge.Entities.Classes;
using WisbooChallenge.Helpers.Resources.Inputs;
using WisbooChallenge.Helpers.Resources.Outputs;

namespace WisbooChallenge.Configuration.Profiles
{
    public class VideoMediaProfile : Profile
    {
        public VideoMediaProfile()
        {
            CreateMap<VideoMedia, VideoMediaModelOutput>();
            CreateMap<VideoMediaModelInput, VideoMedia>();
        }
    }
}