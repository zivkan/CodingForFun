using aoc.csharp._2018;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2018
{
    public class Day01Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2018, 09);
            var (part1, part2) = Day09.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Theory]
        [InlineData("+1\n-2\n+3\n+1", 3)]
        [InlineData("+1\n+1\n+1", 3)]
        [InlineData("+1\n+1\n-2", 0)]
        [InlineData("-1\n-2\n-3", -6)]
        public void Part1Sample(string input, int expected)
        {
            int result = Day01.AccumulateChanges(0, input).Last();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("+1\n-2\n+3\n+1", 2)]
        [InlineData("+1\n-1", 0)]
        [InlineData("+3\n+3\n+4\n-2\n-4", 10)]
        [InlineData("-6\n+3\n+8\n+5\n-6", 5)]
        [InlineData("+7\n+7\n-2\n-7\n-4", 14)]
        public void Part2Sample(string input, int? expected)
        {
            var result = Day01.FirstReoccurance(input);
            Assert.Equal(expected, result);
        }
    }
}
