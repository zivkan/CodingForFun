using aoc.csharp._2019;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day13Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2019, 13);
            var (part1, part2) = Day13.GetAnswer(input);

            Assert.Equal("216", part1);
            Assert.Equal("10025", part2);
        }
    }
}
