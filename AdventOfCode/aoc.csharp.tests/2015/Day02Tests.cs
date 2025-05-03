using aoc.csharp._2015;
using Xunit;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2015
{
    public class Day02Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2015, 02);
            var (part1, part2) = Day02.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Theory]
        [InlineData("2x3x4", 58)]
        [InlineData("1x1x10", 43)]
        public void Part1Sample(string input, int expected)
        {
            int area = Day02.CalculateRequiredArea(input);
            Assert.Equal(expected, area);
        }

        [Theory]
        [InlineData("2x3x4", 34)]
        [InlineData("1x1x10", 14)]
        public void Part2Sample(string input, int expected)
        {
            int area = Day02.CalculateRibbon(input);
            Assert.Equal(expected, area);
        }
    }
}
