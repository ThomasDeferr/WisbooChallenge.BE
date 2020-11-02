using System;
using WisbooChallenge.Entities.Interfaces;

namespace WisbooChallenge.Entities.Classes
{
    public class VideoMedia : IEntity
    {
        public int? ID { get; set; }
        
        public string HashedID { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        
        public DateTime? TS { get; set; }
    }
}