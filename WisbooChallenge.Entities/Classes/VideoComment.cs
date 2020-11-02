using System;
using WisbooChallenge.Entities.Interfaces;

namespace WisbooChallenge.Entities.Classes
{
    public class VideoComment : IEntity
    {
        public int? ID { get; set; }

        public VideoMedia VideoMedia { get; set; }
        public string Content { get; set; }
        public DateTime? UploadDate { get; set; }

        public DateTime? TS { get; set; }
    }
}