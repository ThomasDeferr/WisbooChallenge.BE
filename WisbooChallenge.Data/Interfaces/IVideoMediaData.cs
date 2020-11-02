using System.Collections.Generic;
using System.Threading.Tasks;
using WisbooChallenge.Entities.Classes;

namespace WisbooChallenge.Data.Interfaces
{
    public interface IVideoMediaData
    {
         Task<IEnumerable<VideoMedia>> GetAll();
         Task<VideoMedia> GetByID(int id);
         Task<VideoMedia> GetByHashedID(string hashedId);
         Task<VideoMedia> Insert(VideoMedia videoMedia);
         Task<VideoMedia> Update(VideoMedia videoMedia);
         Task Delete(int id);
    }
}