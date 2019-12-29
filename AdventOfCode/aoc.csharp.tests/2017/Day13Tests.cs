using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day13Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 13);
            var (part1, part2) = Day13.GetAnswer(input);

            Assert.Equal("1300", part1);
            Assert.Equal("3870382", part2);
        }

        private const string _sample = @"0: 3
1: 2
4: 4
6: 4";

        [Fact]
        public void Sample()
        {
            var scanners = Day13.ParseFirewall(_sample);
            var severity = Day13.EnterFirewall(scanners, 0);
            Assert.Equal(24, severity);

            var firstSafeDelay = Day13.FindFirstSafeDelay(scanners);
            Assert.Equal(10, firstSafeDelay);
        }
    }
}
