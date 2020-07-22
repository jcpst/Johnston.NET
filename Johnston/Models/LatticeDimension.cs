using System.Collections.Generic;

namespace Johnston.Models
{
    public class LatticeDimension
    {
        public int Limit { get; set; }
        public IList<Interval> Otonal { get; set; }
        public IList<Interval> Utonal { get; set; }
    }
}