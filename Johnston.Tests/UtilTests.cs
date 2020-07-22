using johnston;
using Xunit;
using Xunit.Abstractions;

namespace Johnston.Tests
{
    public class UtilTests
    {
        private readonly ITestOutputHelper _out;

        public UtilTests(ITestOutputHelper @out)
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
        public void Should_Build_Scale()
        {
            var scale = Util.BuildScale(new[] {3, 5}, 5);

            foreach (var interval in scale)
            {
                _out.WriteLine("{0}", interval.Ratio);
            }
        }
    }
}