using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017
{
    public class Day13Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 13);
            var (part1, part2) = Day13.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
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
