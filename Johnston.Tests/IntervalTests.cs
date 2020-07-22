using johnston;
using Johnston.Models;
using Xunit;
using Xunit.Abstractions;

namespace Johnston.Tests
{
    public class IntervalTests
    {
        private readonly ITestOutputHelper _out;

        public IntervalTests(ITestOutputHelper @out)
        {
            _out = @out;
        }

        [Fact]
        public void Should_Calc_Lattice()
        {
            var result = Util.BuildLattice(new[] {3, 5, 7}, 3);

            foreach (var dimension in result.Dimensions)
            {
                _out.WriteLine("Limit: {0}", dimension.Limit);
                _out.WriteLine("==========");

                _out.WriteLine("- Otonal");
                foreach (var pitch in dimension.Otonal)
                {
                    _out.WriteLine("  - Cents: {0:0000.000} Ratio: {1}", pitch.Cents, pitch.Ratio);
                }

                _out.WriteLine("- Utonal");
                foreach (var pitch in dimension.Utonal)
                {
                    _out.WriteLine("  - Cents: {0:0000.000} Ratio: {1}", pitch.Cents, pitch.Ratio);
                }
            }
        }

        [Fact]
        public void PitchArithmetic()
        {
            // Arrange.
            var major2 = new Interval(9, 8);
            var perfect4 = new Interval(4, 3);
            var perfect5 = new Interval(3, 2);
            var minor7 = new Interval(16, 9);
            var octave = new Interval(2);
            
            // Act.
            var result = perfect5 + perfect5;
            var result2 = major2 - perfect5;
            var result3 = octave - major2;
            var result4 = perfect4 + perfect5;
            
            // Assert.
            Assert.Equal(major2, result);
            Assert.Equal(perfect5, result2);
            Assert.Equal(minor7, result3);
            Assert.Equal(octave, result4);
        }

        [Fact]
        public void RatioShouldAlwaysBeFlattened()
        {
            // Arrange, Act.
            var a = new Interval(18, 5);
            var b = new Interval(3, 5);
            var c = new Interval(1);
            var d = new Interval(2);
            
            // Assert.
            Assert.Equal(new Interval(9, 5), a);
            Assert.Equal(new Interval(6, 5), b);
            Assert.Equal(new Interval(1), c);
            Assert.Equal(new Interval(2), d);
        }

        [Fact]
        public void RecipShouldFlip()
        {
            // Arrange.
            var major3 = new Interval(5, 4);
            
            // Act.
            var result = !major3;
            
            // Assert.
            Assert.Equal(new Interval(8, 5), result);
        }
    }
}