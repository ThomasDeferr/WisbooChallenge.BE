using System.ComponentModel.DataAnnotations;

namespace WisbooChallenge.Helpers.Resources.Inputs
{
    public class VideoMediaModelInput
    {
        [Required]
        public string HashedID { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Color { get; set; }
    }
}