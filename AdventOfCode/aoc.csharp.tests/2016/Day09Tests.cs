using aoc.csharp._2016;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day09Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2016, 09);
            var (part1, part2) = Day09.GetAnswer(input);

            Assert.Equal("150914", part1);
            Assert.Equal("11052855125", part2);
        }

        [Theory]
        [InlineData("ADVENT", 6)]
        [InlineData("A(1x5)BC", 7)]
        [InlineData("(3x3)XYZ", 9)]
        [InlineData("A(2x2)BCD(2x2)EFG", 11)]
        [InlineData("(6x1)(1x3)A", 6)]
        [InlineData("X(8x2)(3x3)ABCY", 18)]
        public void Part1Samples(string input, int expected)
        {
            int actual = Day09.CalculateLength(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("(3x3)XYZ", 9)]
        [InlineData("X(8x2)(3x3)ABCY", 20)]
        [InlineData("(27x12)(20x12)(13x14)(7x10)(1x12)A", 241920)]
        [InlineData("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", 445)]
        public void Part2Samples(string input, int expected)
        {
            long actual = Day09.CalculateLengthRecursive(input);
            Assert.Equal(expected, actual);
        }
    }
}
