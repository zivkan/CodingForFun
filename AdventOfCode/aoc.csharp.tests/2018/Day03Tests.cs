using aoc.csharp._2018;
using Xunit;

namespace aoc.csharp.tests._2018
{
    public class Day03Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2018, 03);
            var (part1, part2) = Day03.GetAnswer(input);

            Assert.Equal("116920", part1);
            Assert.Equal("382", part2);
        }

        [Fact]
        public void Sample()
        {
            string input =
                "#1 @ 1,3: 4x4\n" +
                "#2 @ 3,1: 4x4\n" +
                "#3 @ 5,5: 2x2";
            var (overlaps, intact) = Day03.CountOverlappedSquares(input);
            Assert.Equal(4, overlaps);
            Assert.Equal(3, intact);
        }
    }
}
