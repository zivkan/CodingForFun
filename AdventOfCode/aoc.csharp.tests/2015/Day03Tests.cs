using aoc.csharp._2015;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day03Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 03);
            var (part1, part2) = Day03.GetAnswer(input);

            Assert.Equal("2081", part1);
            Assert.Equal("2341", part2);
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
