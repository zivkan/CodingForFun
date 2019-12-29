using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day04Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 04);
            var (part1, part2) = Day04.GetAnswer(input);

            Assert.Equal("466", part1);
            Assert.Equal("251", part2);
        }

        [Theory]
        [InlineData("aa bb cc dd ee", 1)]
        [InlineData("aa bb cc dd aa", 0)]
        [InlineData("aa bb cc dd aaa", 1)]
        public void Part1Sample(string input, int expected)
        {
            int valid = Day04.CountValidPhrases(input);
            Assert.Equal(expected, valid);
        }

        [Theory]
        [InlineData("abcde fghij", 1)]
        [InlineData("abcde xyz ecdab", 0)]
        [InlineData("a ab abc abd abf abj", 1)]
        [InlineData("iiii oiii ooii oooi oooo", 1)]
        [InlineData("oiii ioii iioi iiio", 0)]
        public void Part2Sample(string input, int expected)
        {
            int valid = Day04.CountValidPhrases2(input);
            Assert.Equal(expected, valid);
        }
    }
}
