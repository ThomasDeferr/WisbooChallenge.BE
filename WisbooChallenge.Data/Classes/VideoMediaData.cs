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
    public class VideoMediaData : IVideoMediaData
    {
        private readonly IDBManager _dbManager;

        public VideoMediaData(IDBManager dbManager)
        {
            _dbManager = dbManager;
        }

        #region Public methods
        public async Task<IEnumerable<VideoMedia>> GetAll()
        {
            IEnumerable<DataRow> result = await _dbManager.Get(storedProcedure: "usp_VideoMedias_GetAll");
            return Mapper(result);
        }

        public async Task<VideoMedia> GetByID(int id)
        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@ID", id));
            #endregion
            
            DataRow result = await _dbManager.GetSingle(storedProcedure: "usp_VideoMedias_GetByID", sqlParameters: sqlParameters);
            return Mapper(result);
        }


        public async Task<VideoMedia> GetByHashedID(string code)
        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@HashedID", code));
            #endregion
            
            DataRow result = await _dbManager.GetSingle(storedProcedure: "usp_VideoMedias_GetByHashedID", sqlParameters: sqlParameters);
            return Mapper(result);
        }

        public async Task<VideoMedia> Insert(VideoMedia videoMedia)
        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(_dbManager.CreateInputParameter("@HashedID", videoMedia.HashedID));
            sqlParameters.Add(_dbManager.CreateInputParameter("@Title", videoMedia.Title));
            sqlParameters.Add(_dbManager.CreateInputParameter("@Color", videoMedia.Color));
            sqlParameters.Add(_dbManager.CreateInputParameter("@ThumbnailUrl", videoMedia.ThumbnailUrl));

            SqlParameter ouputParameterId = _dbManager.CreateOutputParameter("@ID", SqlDbType.Int);
            sqlParameters.Add(ouputParameterId);
            #endregion

            await _dbManager.Insert(storedProcedure: "usp_VideoMedias_Insert", sqlParameters: sqlParameters);

            videoMedia.ID = (int)ouputParameterId.Value;

            return videoMedia;
        }
        public async Task<VideoMedia> Update(VideoMedia videoMedia)

        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(_dbManager.CreateInputParameter("@ID", videoMedia.ID));

            sqlParameters.Add(_dbManager.CreateInputParameter("@HashedID", videoMedia.HashedID));
            sqlParameters.Add(_dbManager.CreateInputParameter("@Title", videoMedia.Title));
            sqlParameters.Add(_dbManager.CreateInputParameter("@Color", videoMedia.Color));
            sqlParameters.Add(_dbManager.CreateInputParameter("@ThumbnailUrl", videoMedia.ThumbnailUrl));
            #endregion

            await _dbManager.Update(storedProcedure: "usp_VideoMedias_UpdateByID", sqlParameters: sqlParameters);

            return videoMedia;
        }

        public async Task Delete(int id)
        {
            #region Generate SqlParameters
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(_dbManager.CreateInputParameter("@ID", id));
            #endregion

            await _dbManager.Delete(storedProcedure: "usp_VideoMedias_Delete", sqlParameters: sqlParameters);
        }
        #endregion

        
        protected IEnumerable<VideoMedia> Mapper(IEnumerable<DataRow> reader)
        {
            if (reader != null && reader.Any())
                return reader.Select(r => DataMapper(r)).ToList();
            else
                return Enumerable.Empty<VideoMedia>();
        }
        protected VideoMedia Mapper(DataRow reader)
        {
            if (reader != null)
                return DataMapper(reader);
            else
                return default;
        }

        private VideoMedia DataMapper(DataRow reader)
        {
            return new VideoMedia()
            {
                ID = reader.GetValue<int>(nameof(VideoMedia.ID)),

                HashedID = reader.GetValue<string>(nameof(VideoMedia.HashedID)),
                Title = reader.GetValue<string>(nameof(VideoMedia.Title)),
                Color = reader.GetValue<string>(nameof(VideoMedia.Color)),
                ThumbnailUrl = reader.GetValue<string>(nameof(VideoMedia.ThumbnailUrl)),
                
                TS = reader.GetValue<DateTime>(nameof(VideoMedia.TS))
            };
        }
    }
}