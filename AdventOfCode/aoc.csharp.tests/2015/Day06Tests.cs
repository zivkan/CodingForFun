using aoc.csharp._2015;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day06Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 06);
            var (part1, part2) = Day06.GetAnswer(input);

            Assert.Equal("400410", part1);
            Assert.Equal("15343601", part2);
        }
    }
}
