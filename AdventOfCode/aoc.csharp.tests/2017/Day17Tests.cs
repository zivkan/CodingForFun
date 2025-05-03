using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017
{
    public class Day17Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 17);
            var (part1, part2) = Day17.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Fact]
        public void Sample()
        {
            var buffer = Day17.SpinLock(2017, 3);
            var value = Day17.ValueAfter(buffer, 2017);
            Assert.Equal(638, value);
        }
    }
}
