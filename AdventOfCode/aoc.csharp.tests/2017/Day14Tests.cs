using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day14Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 14);
            var (part1, part2) = Day14.GetAnswer(input);

            Assert.Equal("8074", part1);
            Assert.Equal("1212", part2);
        }

        [Fact]
        public void Sample()
        {
            string input = "flqrgnkx";
            var used = Day14.GetUsed(input);
            Assert.Equal(8108, used.Count);

            var groups = Day14.Group(used);
            Assert.Equal(1242, groups.Count);
        }
    }
}
