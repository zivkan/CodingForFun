using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day11Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 11);
            var (part1, part2) = Day11.GetAnswer(input);

            Assert.Equal("696", part1);
            Assert.Equal("1461", part2);
        }

        [Theory]
        [InlineData("ne,ne,ne", 3)]
        [InlineData("ne,ne,sw,sw", 0)]
        [InlineData("ne,ne,s,s", 2)]
        [InlineData("se,sw,se,sw,sw", 3)]
        public void Sample(string input, int expected)
        {
            var (last, _) = Day11.GetDistances(input);
            Assert.Equal(expected, last);
        }
    }
}
