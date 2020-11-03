using AutoMapper;
using WisbooChallenge.Entities.Classes;
using WisbooChallenge.Helpers.Resources.Inputs;
using WisbooChallenge.Helpers.Resources.Outputs;

namespace WisbooChallenge.Configuration.Profiles
{
    public class VideoCommentProfile : Profile
    {
        public VideoCommentProfile()
        {
            CreateMap<VideoComment, VideoCommentModelOutput>();
            CreateMap<VideoCommentModelInput, VideoComment>();
        }
    }
}