using aoc.csharp._2020;
using System.IO;
using Xunit;

namespace aoc.csharp.tests._2020
{
    public class Day01Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2020, 01);
            var (part1, part2) = Day01.GetAnswer(input);

            Assert.Equal("1007104", part1);
            Assert.Equal("18847752", part2);
        }

        [Fact]
        public void Sample()
        {
            var input = new StringReader(@"1721
979
366
299
675
1456");
            var (part1, part2) = Day01.GetAnswer(input);

            Assert.Equal("514579", part1);
            Assert.Equal("241861950", part2);
        }
    }
}
