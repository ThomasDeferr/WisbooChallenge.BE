using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WisbooChallenge.Data.Core;
using WisbooChallenge.Data.Interfaces;
using WisbooChallenge.Entities.Classes;
using WisbooChallenge.Helpers.Extensions;

namespace WisbooChallenge.Data.Classes
{
    public class VideoCommentData : IVideoCommentData
    {
        private readonly IDBManager _dbManager;

        public VideoCommentData(IDBManager dbManager)
        {
            _dbManager = dbManager;
        }

        #region Public methods
        public async Task<IEnumerable<VideoComment>> GetAll()
        {
            IEnumerable<DataRow> result = await _dbManager.Get(storedProcedure: "usp_VideoComments_GetAll");
            return Mapper(result);
        }

        public async Task<IEnumerable<VideoComment>> GetAllByVideoMedia(int videoMediaID)
        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@VideoMediaID", videoMediaID));
            #endregion

            IEnumerable<DataRow> result = await _dbManager.Get(storedProcedure: "usp_VideoComments_GetAllByVideoMedia", sqlParameters: sqlParameters);
            return Mapper(result);
        }

        public async Task<VideoComment> GetByID(int id)
        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@ID", id));
            #endregion
            
            DataRow result = await _dbManager.GetSingle(storedProcedure: "usp_VideoComments_GetById", sqlParameters: sqlParameters);
            return Mapper(result);
        }

        public async Task<VideoComment> Insert(VideoComment videoComment)
        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(_dbManager.CreateInputParameter("@VideoMediaID", videoComment.VideoMedia.ID));
            sqlParameters.Add(_dbManager.CreateInputParameter("@Content", videoComment.Content));
            sqlParameters.Add(_dbManager.CreateInputParameter("@UploadDate", videoComment.UploadDate));

            SqlParameter ouputParameterId = _dbManager.CreateOutputParameter("@ID", SqlDbType.Int);
            sqlParameters.Add(ouputParameterId);
            #endregion

            await _dbManager.Insert(storedProcedure: "usp_VideoComments_Insert", sqlParameters: sqlParameters);

            videoComment.ID = (int)ouputParameterId.Value;

            return videoComment;
        }
        public async Task<VideoComment> Update(VideoComment videoComment)

        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(_dbManager.CreateInputParameter("@ID", videoComment.ID));

            sqlParameters.Add(_dbManager.CreateInputParameter("@VideoMediaID", videoComment.VideoMedia.ID));
            sqlParameters.Add(_dbManager.CreateInputParameter("@Content", videoComment.Content));
            sqlParameters.Add(_dbManager.CreateInputParameter("@UploadDate", videoComment.UploadDate));
            #endregion

            await _dbManager.Update(storedProcedure: "usp_VideoComments_UpdateByID", sqlParameters: sqlParameters);

            return videoComment;
        }

        public async Task Delete(int id)
        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(_dbManager.CreateInputParameter("@ID", id));
            #endregion

            await _dbManager.Delete(storedProcedure: "usp_VideoComments_Delete", sqlParameters: sqlParameters);
        }
        #endregion

        
        protected IEnumerable<VideoComment> Mapper(IEnumerable<DataRow> reader)
        {
            if (reader != null && reader.Any())
                return reader.Select(r => DataMapper(r)).ToList();
            else
                return Enumerable.Empty<VideoComment>();
        }
        protected VideoComment Mapper(DataRow reader)
        {
            if (reader != null)
                return DataMapper(reader);
            else
                return default;
        }

        private VideoComment DataMapper(DataRow reader)
        {
            return new VideoComment()
            {
                ID = reader.GetValue<int>(nameof(VideoComment.ID)),

                Content = reader.GetValue<string>(nameof(VideoComment.Content)),
                UploadDate = reader.GetValue<DateTime?>(nameof(VideoComment.UploadDate)),
                VideoMedia = new VideoMedia()
                {
                    ID = reader.GetValue<int>(nameof(VideoMedia) + nameof(VideoMedia.ID))
                },
                
                TS = reader.GetValue<DateTime>(nameof(VideoComment.TS))
            };
        }
    }
}