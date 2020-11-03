using System.ComponentModel.DataAnnotations;

namespace WisbooChallenge.Helpers.Resources.Inputs
{
    public class VideoCommentModelInput
    {
        [Required]
        public string Content { get; set; }
    }
}