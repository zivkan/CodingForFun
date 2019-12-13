using aoc.csharp._2019;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day04Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2019, 04);
            var (part1, part2) = Day04.GetAnswer(input);

            Assert.Equal("2814", part1);
            Assert.Equal("1991", part2);
        }
    }
}
