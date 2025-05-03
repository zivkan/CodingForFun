using aoc.csharp._2015;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2015
{
    public class Day03Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2015, 03);
            var (part1, part2) = Day03.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Theory]
        [InlineData(">", 2)]
        [InlineData("^>v<", 4)]
        [InlineData("^v^v^v^v^v", 2)]
        public void Part1Sample(string input, int expected)
        {
            int houses = Day03.DeliverPresents(input, 1);
            Assert.Equal(expected, houses);
        }

        [Theory]
        [InlineData("^v", 3)]
        [InlineData("^>v<", 3)]
        [InlineData("^v^v^v^v^v", 11)]
        public void Part2Sample(string input, int expected)
        {
            int houses = Day03.DeliverPresents(input, 2);
            Assert.Equal(expected, houses);
        }
    }
}
