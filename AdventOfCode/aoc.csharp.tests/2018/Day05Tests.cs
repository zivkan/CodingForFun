using aoc.csharp._2018;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2018
{
    public class Day05Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2018, 05);
            var (part1, part2) = Day05.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        private static readonly string _sampleInput = "dabAcCaCBAcCcaDA";

        [Fact]
        public void Part1Sample()
        {
            var result = Day05.React(_sampleInput);
            Assert.Equal(10, result.Length);
        }

        [Fact]
        public void Part2Sample()
        {
            var result = Day05.BestReaction(_sampleInput);
            Assert.Equal(4, result.Length);
        }
    }
}
