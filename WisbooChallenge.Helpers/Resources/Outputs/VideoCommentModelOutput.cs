using System;

namespace WisbooChallenge.Helpers.Resources.Outputs
{
    public class VideoCommentModelOutput
    {
        public int ID { get; set; }

        public int VideoMediaID { get; set; }
        public string Content { get; set; }
        public DateTime UploadDate { get; set; }

        public DateTime TS { get; set; }
    }
}