using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day03Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 03);
            var (part1, part2) = Day03.GetAnswer(input);

            Assert.Equal("326", part1);
            Assert.Equal("363010", part2);
        }

        [Theory]
        [InlineData("1", 0)]
        [InlineData("12", 3)]
        [InlineData("23", 2)]
        [InlineData("1024", 31)]
        public void Part1Samples(string input, int expected)
        {
            int distance = Day03.ManhattanDistance(input);
            Assert.Equal(expected, distance);
        }
    }
}
