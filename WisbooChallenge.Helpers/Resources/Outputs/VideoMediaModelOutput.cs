using System;
using System.Collections.Generic;

namespace WisbooChallenge.Helpers.Resources.Outputs
{
    public class VideoMediaModelOutput
    {
        public int ID { get; set; }

        public string HashedID { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string ThumbnailUrl { get; set; }
        public IEnumerable<VideoCommentModelOutput> Comments { get; set; }

        public DateTime TS { get; set; }
    }
}