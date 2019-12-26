using aoc.csharp._2015;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day02Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 02);
            var (part1, part2) = Day02.GetAnswer(input);

            Assert.Equal("1606483", part1);
            Assert.Equal("3842356", part2);
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
