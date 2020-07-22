using System;
using System.Collections.Generic;
using System.Linq;
using Johnston.Models;

namespace johnston
{
    public static class Util
    {
        /// <summary>
        /// Build an n-dimensional pitch lattice.
        /// </summary>
        /// <param name="dimensions"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static Lattice BuildLattice(IEnumerable<int> dimensions, int steps)
        {
            return new Lattice
            {
                Dimensions = dimensions.Select(DimensionBuilder(steps))
            };
        }

        /// <summary>
        /// Build an ordered scale from the Intervals in a Lattice.
        /// </summary>
        /// <param name="lattice"></param>
        /// <returns></returns>
        public static IEnumerable<Interval> BuildScale(Lattice lattice)
        {
            return BuildScale(lattice.Dimensions.Select(d => d.Limit), lattice.Dimensions.First().Otonal.Count);
        }

        /// <summary>
        /// Build an ordered scale from the Intervals in a Lattice.
        /// </summary>
        /// <param name="dimensions"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static IEnumerable<Interval> BuildScale(IEnumerable<int> dimensions, int steps)
        {
            return dimensions
                .Select(DimensionBuilder(steps))
                .Select(ld => ld.Otonal.Concat(ld.Utonal))
                .SelectMany(d => d)
                .Distinct()
                .OrderBy(i => i.Cents);
        }

        private static Func<int, LatticeDimension> DimensionBuilder(int steps)
        {
            return dimension => new LatticeDimension
            {
                Limit = dimension,
                Otonal = Walk(new Interval(dimension), steps),
                Utonal = Walk(!new Interval(dimension), steps)
            };
        }

        private static IList<Interval> Walk(Interval interval, int steps)
        {
            var pitches = new List<Interval> {new Interval(1)};

            foreach (var _ in Enumerable.Range(1, steps))
            {
                var lastPitch = pitches.Last();
                var nextPitch = lastPitch + interval;

                pitches.Add(nextPitch);
            }

            return pitches;
        }
    }
}