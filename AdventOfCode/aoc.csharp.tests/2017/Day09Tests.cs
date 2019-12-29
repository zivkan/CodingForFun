using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day09Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 09);
            var (part1, part2) = Day09.GetAnswer(input);

            Assert.Equal("12803", part1);
            Assert.Equal("6425", part2);
        }

        [Theory]
        [InlineData("{}", 1)]
        [InlineData("{{{}}}", 6)]
        [InlineData("{{},{}}", 5)]
        [InlineData("{{{},{},{{}}}}", 16)]
        [InlineData("{<a>,<a>,<a>,<a>}", 1)]
        [InlineData("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)]
        [InlineData("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9)]
        [InlineData("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
        public void Part1Sample(string input, int expected)
        {
            var (score, _) = Day09.GetScore(input);
            Assert.Equal(expected, score);
        }

        [Theory]
        [InlineData("<>", 0)]
        [InlineData("<random characters>", 17)]
        [InlineData("<<<<>", 3)]
        [InlineData("<{!>}>", 2)]
        [InlineData("<!!>", 0)]
        [InlineData("<!!!>>", 0)]
        [InlineData("<{o\"i!a,<{i<a>", 10)]
        public void Part2Sample(string input, int expected)
        {
            var (_, chars) = Day09.GetScore(input);
            Assert.Equal(expected, chars);
        }
    }
}
