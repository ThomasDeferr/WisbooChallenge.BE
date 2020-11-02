using System.Collections.Generic;
using System.Threading.Tasks;
using WisbooChallenge.Entities.Classes;

namespace WisbooChallenge.Data.Interfaces
{
    public interface IVideoCommentData
    {
        Task<IEnumerable<VideoComment>> GetAll();
        Task<IEnumerable<VideoComment>> GetAllByVideoMedia(int videoMediaID);
        Task<VideoComment> GetByID(int id);
        Task<VideoComment> Insert(VideoComment videoComment);
        Task<VideoComment> Update(VideoComment videoComment);
        Task Delete(int id);
    }
}