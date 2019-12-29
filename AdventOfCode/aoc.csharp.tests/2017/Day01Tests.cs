using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day01Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 01);
            var (part1, part2) = Day01.GetAnswer(input);

            Assert.Equal("1044", part1);
            Assert.Equal("1054", part2);
        }

        [Theory]
        [InlineData("1122", 3)]
        [InlineData("1111", 4)]
        [InlineData("1234", 0)]
        [InlineData("91212129", 9)]
        public void Part1Sample(string input, int expected)
        {
            int result = Day01.SolveCaptcha(input, Day01.Next);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1212", 6)]
        [InlineData("1221", 0)]
        [InlineData("123425", 4)]
        [InlineData("123123", 12)]
        [InlineData("12131415", 4)]
        public void Part2Sample(string input, int expected)
        {
            int result = Day01.SolveCaptcha(input, Day01.Opposite);
            Assert.Equal(expected, result);
        }
    }
}
