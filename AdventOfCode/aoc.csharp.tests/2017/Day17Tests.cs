using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day17Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 17);
            var (part1, part2) = Day17.GetAnswer(input);

            Assert.Equal("1311", part1);
            Assert.Equal("39170601", part2);
        }

        [Fact]
        public void Sample()
        {
            var buffer = Day17.SpinLock(2017, 3);
            var value = Day17.ValueAfter(buffer, 2017);
            Assert.Equal(638, value);
        }
    }
}
