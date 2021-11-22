using aoc.csharp._2015;
using System.Linq;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day10Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 10);
            var (part1, part2) = Day10.GetAnswer(input);

            Assert.Equal("252594", part1);
            Assert.Equal("3579328", part2);
        }

        [Theory]
        [InlineData("1", "11")]
        [InlineData("11", "21")]
        [InlineData("21", "1211")]
        [InlineData("1211", "111221")]
        [InlineData("111221", "312211")]
        public void Part1(string input, string expected)
        {
            var actual = Day10.Game(input).First();
            Assert.Equal(expected, actual);
        }
    }
}
