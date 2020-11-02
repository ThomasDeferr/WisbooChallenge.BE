using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WisbooChallenge.Entities.Classes;
using WisbooChallenge.Helpers.Attributes;
using WisbooChallenge.Helpers.Resources.Inputs;
using WisbooChallenge.Helpers.Resources.Outputs;
using WisbooChallenge.Data.Interfaces;

namespace WisbooChallenge.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class VideoMediasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVideoMediaData _videoMediaData;
        private readonly IVideoCommentData _videoCommentData;

        public VideoMediasController(IMapper mapper, IVideoMediaData videoMediaData, IVideoCommentData videoCommentData)
        {
            _mapper = mapper;
            _videoMediaData = videoMediaData;
            _videoCommentData = videoCommentData;
        }

        // GET: v1/videoMedias
        [HttpGet(Name = "GetAllVideoMedias")]
        public async Task<ActionResult<PagingModelOutput<VideoMediaModelOutput>>> GetAll([FromQuery][NotNegative] int offset = 0, [FromQuery][NotNegative] int limit = 50)
        {
            IEnumerable<VideoMedia> videoMedias = await _videoMediaData.GetAll();
            IEnumerable<VideoComment> videoComments = await _videoCommentData.GetAll();
            
            IEnumerable<VideoMedia> videoMediasFiltered = videoMedias.Skip(offset).Take(limit);
            IEnumerable<VideoMediaModelOutput> videoMediasOutput = _mapper.Map<IEnumerable<VideoMediaModelOutput>>(videoMediasFiltered);

            foreach (VideoMediaModelOutput vmOutput in videoMediasOutput)
            {
                IEnumerable<VideoComment> commentsByVideoMedia = videoComments.Where(c => c.VideoMedia?.ID == vmOutput.ID);
                vmOutput.Comments = _mapper.Map<IEnumerable<VideoCommentModelOutput>>(commentsByVideoMedia);
            }
            
            PagingModelOutput<VideoMediaModelOutput> result = new PagingModelOutput<VideoMediaModelOutput>()
            {
                Paging = new PagingOutput(total: videoMedias.Count(), offset: offset, limit: limit),
                Results = videoMediasOutput
            };

            return Ok(result);
        }

        // GET: v1/videoMedias/{id}
        [HttpGet("{id}", Name = "GetVideoMediaByID")]
        public async Task<ActionResult<VideoMediaModelOutput>> GetByID([FromRoute] int id)
        {
            VideoMedia videoMedia = await _videoMediaData.GetByID(id: id);
            if (videoMedia == null) 
                return new NotFoundObjectResult("This video does not exist.");
                
            IEnumerable<VideoComment> videoComments = await _videoCommentData.GetAllByVideoMedia(videoMediaID: videoMedia.ID.Value);

            VideoMediaModelOutput videoMediaOutput = _mapper.Map<VideoMediaModelOutput>(videoMedia);
            videoMediaOutput.Comments = _mapper.Map<IEnumerable<VideoCommentModelOutput>>(videoComments);

            return Ok(videoMediaOutput);
        }

        // POST: v1/videoMedias
        [HttpPost(Name = "InsertVideoMedia")]
        public async Task<ActionResult<VideoMediaModelOutput>> Insert([FromBody] VideoMediaModelInput videoMediaInput)
        {
            VideoMedia videoMediaDBDuplicated = await _videoMediaData.GetByHashedID(hashedId: videoMediaInput.HashedID);
            if (videoMediaDBDuplicated != null)
                return new ConflictObjectResult("A video with same hashedId already exists");

            VideoMedia videoMedia = _mapper.Map<VideoMedia>(videoMediaInput);
            VideoMedia videoMediaInserted = await _videoMediaData.Insert(videoMedia: videoMedia);

            VideoMediaModelOutput videoMediaInsertedOutput = _mapper.Map<VideoMediaModelOutput>(videoMediaInserted);

            return new CreatedAtRouteResult("GetVideoMediaByID", new { id = videoMediaInsertedOutput.ID }, videoMediaInsertedOutput);
        }

        // PUT: v1/videoMedias/{id}
        [HttpPut("{id}", Name = "UpdateVideoMedia")]
        public async Task<ActionResult<VideoMediaModelOutput>> Update([FromRoute] int id, [FromBody] VideoMediaModelInput videoMediaInput)
        {
            VideoMedia videoMediaDB = await _videoMediaData.GetByID(id: id);
            if (videoMediaDB == null)
                return new NotFoundObjectResult("This video does not exist.");

            VideoMedia videoMediaDBDuplicated = await _videoMediaData.GetByHashedID(hashedId: videoMediaInput.HashedID);
            if (videoMediaDBDuplicated != null && videoMediaDBDuplicated.ID != videoMediaDB.ID)
                return new ConflictObjectResult("A video with same hashedId already exists");

            VideoMedia videoMedia = _mapper.Map<VideoMedia>(videoMediaInput);
            videoMedia.ID = videoMediaDB.ID;

            VideoMedia videoMediaUpdated = await _videoMediaData.Update(videoMedia: videoMedia);

            VideoMediaModelOutput videoMediaUpdatedOutput = _mapper.Map<VideoMediaModelOutput>(videoMediaUpdated);

            return new CreatedAtRouteResult("GetVideoMediaByID", new { id = videoMediaUpdatedOutput.ID }, videoMediaUpdatedOutput);
        }

        // DELETE: v1/videoMedias/{id}
        [HttpDelete("{id}", Name = "DeleteVideoMedia")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            VideoMedia videoMediaDB = await _videoMediaData.GetByID(id: id);
            if (videoMediaDB == null)
                return new NotFoundObjectResult("This video does not exist.");

            await _videoMediaData.Delete(id: id);

            return NoContent();
        }
    }
}
