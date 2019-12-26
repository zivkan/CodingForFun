using aoc.csharp._2016;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day03Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2016, 03);
            var (part1, part2) = Day03.GetAnswer(input);

            Assert.Equal("917", part1);
            Assert.Equal("1649", part2);
        }
    }
}
