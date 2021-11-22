using aoc.csharp._2015;
using System.Linq;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day11Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 11);
            var (part1, part2) = Day11.GetAnswer(input);

            Assert.Equal("cqjxxyzz", part1);
            Assert.Equal("cqkaabcc", part2);
        }

        [Theory]
        [InlineData("hijklmmn", false)]
        [InlineData("abbceffg", false)]
        [InlineData("abbcegjk", false)]
        [InlineData("abcdffaa", true)]
        [InlineData("ghjaabcc", true)]
        public void IsValidTests(string input, bool expected)
        {
            var actual = Day11.IsValid(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("abcdefgh", "abcdffaa")]
        [InlineData("ghijklmn", "ghjaabcc")]
        public void NextTests(string input, string expected)
        {
            var actual = Day11.Next(input);
            Assert.Equal(expected, actual);
        }
    }
}
