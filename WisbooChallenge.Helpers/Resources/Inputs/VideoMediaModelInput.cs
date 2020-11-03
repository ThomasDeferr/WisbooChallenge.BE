using System.ComponentModel.DataAnnotations;

namespace WisbooChallenge.Helpers.Resources.Inputs
{
    public class VideoMediaModelInput
    {
        [Required]
        [MaxLength(25)]
        public string HashedID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public string Color { get; set; }

        [Required]
        [MaxLength(250)]
        public string ThumbnailUrl { get; set; }
    }
}