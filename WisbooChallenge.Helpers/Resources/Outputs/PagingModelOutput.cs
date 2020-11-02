using System.Collections.Generic;

namespace WisbooChallenge.Helpers.Resources.Outputs
{
    public class PagingModelOutput<T>
    {
        public PagingOutput Paging { get; set; }
        public IEnumerable<T> Results { get; set; }
    }

    public class PagingOutput 
    {
        public int Total { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }

        public PagingOutput(int total, int offset, int limit)
        {
            Total = total;
            Offset = offset;
            Limit = limit;
        }
    }
}