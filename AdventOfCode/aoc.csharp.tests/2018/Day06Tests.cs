using aoc.csharp._2018;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2018
{
    public class Day06Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2018, 06);
            var (part1, part2) = Day06.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        private static readonly string _sampleInput = 
            "1, 1\n" +
            "1, 6\n" +
            "8, 3\n" +
            "3, 4\n" +
            "5, 5\n" +
            "8, 9";

        [Fact]
        public void Part1Sample()
        {
            var result = Day06.FindLargestArea(_sampleInput);
            Assert.Equal(17, result);
        }

        [Fact]
        public void Part2Sample()
        {
            var result = Day06.SizeOfTargetArea(_sampleInput, 32);
            Assert.Equal(16, result);
        }
    }
}
