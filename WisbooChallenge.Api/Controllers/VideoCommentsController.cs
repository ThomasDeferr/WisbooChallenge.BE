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
    [Route("v1/videomedias/{videoMediaId}/comments")]
    public class VideoCommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVideoMediaData _videoMediaData;
        private readonly IVideoCommentData _videoCommentData;

        public VideoCommentsController(IMapper mapper, IVideoMediaData videoMediaData, IVideoCommentData videoCommentData)
        {
            _mapper = mapper;
            _videoMediaData = videoMediaData;
            _videoCommentData = videoCommentData;
        }

        // GET: v1/videomedias/{videoMediaId}/comments
        [HttpGet(Name = "GetAllCommentsByVideoMedia")]
        public async Task<ActionResult<PagingModelOutput<VideoCommentModelOutput>>> GetAll([FromRoute] int videoMediaId, [FromQuery][NotNegative] int offset = 0, [FromQuery][NotNegative] int limit = 50)
        {
            IEnumerable<VideoComment> videoComments = await _videoCommentData.GetAllByVideoMedia(videoMediaID: videoMediaId);
            
            IEnumerable<VideoComment> videoCommentsFiltered = videoComments.Skip(offset).Take(limit);
            IEnumerable<VideoCommentModelOutput> videoCommentsOutput = _mapper.Map<IEnumerable<VideoCommentModelOutput>>(videoCommentsFiltered);
            
            PagingModelOutput<VideoCommentModelOutput> result = new PagingModelOutput<VideoCommentModelOutput>()
            {
                Paging = new PagingOutput(total: videoComments.Count(), offset: offset, limit: limit),
                Results = videoCommentsOutput
            };

            return Ok(result);
        }

        // GET: v1/videomedias/{videoMediaId}/comments/{id}
        [HttpGet("{id}", Name = "GetVideoCommentByID")]
        public async Task<ActionResult<VideoMediaModelOutput>> GetByID([FromRoute] int videoMediaId, [FromRoute] int id)
        {
            VideoMedia videoMediaDB = await _videoMediaData.GetByID(id: videoMediaId);
            if (videoMediaDB == null)
                return new NotFoundObjectResult("This video does not exist.");

            VideoComment videoComment = await _videoCommentData.GetByID(id: id);
            if (videoComment == null || videoComment.VideoMedia?.ID != videoMediaDB.ID) 
                return new NotFoundObjectResult("This comment does not exist.");

            VideoCommentModelOutput videoCommentOutput = _mapper.Map<VideoCommentModelOutput>(videoComment);

            return Ok(videoCommentOutput);
        }

        // POST: v1/videomedias/{videoMediaId}/comments
        [HttpPost(Name = "InsertVideoComment")]
        public async Task<ActionResult<VideoCommentModelOutput>> Insert([FromRoute] int videoMediaId, [FromBody] VideoCommentModelInput videoCommentInput)
        {
            VideoMedia videoMediaDB = await _videoMediaData.GetByID(id: videoMediaId);
            if (videoMediaDB == null)
                return new NotFoundObjectResult("This video does not exist.");

            VideoComment videoComment = _mapper.Map<VideoComment>(videoCommentInput);
            videoComment.VideoMedia = videoMediaDB;
            videoComment.UploadDate = DateTime.Now;
            
            VideoComment videoCommentInserted = await _videoCommentData.Insert(videoComment: videoComment);

            VideoCommentModelOutput videoCommentInsertedOutput = _mapper.Map<VideoCommentModelOutput>(videoCommentInserted);

            return new CreatedAtRouteResult(
                "GetVideoCommentByID", 
                new { videoMediaId = videoCommentInsertedOutput.VideoMediaID, id = videoCommentInsertedOutput.ID }, 
                videoCommentInsertedOutput
            );
        }

        // PUT: v1/videomedias/{videoMediaId}/comments/{id}
        [HttpPut("{id}", Name = "UpdateVideoComment")]
        public Task<ActionResult<VideoCommentModelOutput>> Update([FromRoute] int videoMediaId, [FromRoute] int id, [FromBody] VideoCommentModelInput videoCommentInput)
        {
            throw new NotSupportedException();
        }

        // DELETE: v1/videomedias/{videoMediaId}/comments/{id}
        [HttpDelete("{id}", Name = "DeleteVideoComment")]
        public Task<ActionResult> Delete([FromRoute] int id)
        {
            throw new NotSupportedException();
        }
    }
}
