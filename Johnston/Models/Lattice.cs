using System.Collections.Generic;

namespace Johnston.Models
{
    public class Lattice
    {
        public IEnumerable<LatticeDimension> Dimensions { get; set; }

        public Lattice()
        {
            Dimensions = new List<LatticeDimension>();
        }
    }
}