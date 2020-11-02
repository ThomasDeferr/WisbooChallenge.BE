using System.ComponentModel.DataAnnotations;

namespace WisbooChallenge.Helpers.Attributes
{
    public class NotNegativeAttribute : RangeAttribute
    {
        public NotNegativeAttribute()
            : base(minimum: 0, maximum: double.MaxValue)
        {
            
        }
    }
}