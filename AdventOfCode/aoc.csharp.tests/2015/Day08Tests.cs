using aoc.csharp._2015;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day08Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 08);
            var (part1, part2) = Day08.GetAnswer(input);

            Assert.Equal("1350", part1);
            Assert.Equal("2085", part2);
        }

        [Theory]
        [InlineData(@"""""", "")]
        [InlineData(@"""abc""", "abc")]
        [InlineData(@"""aaa\""aaa""", "aaa\"aaa")]
        [InlineData(@"""\x27""", "'")]
        public void Part1Test(string input, string expected)
        {
            var actual = Day08.ParseLine(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("\"\"", "\"\\\"\\\"\"")]
        [InlineData("\"abc\"", "\"\\\"abc\\\"\"")]
        [InlineData("\"aaa\\\"aaa\"", "\"\\\"aaa\\\\\\\"aaa\\\"\"")]
        [InlineData("\"\\x27\"", "\"\\\"\\\\x27\\\"\"")]
        public void Part2Test(string input, string expected)
        {
            var actual = Day08.EncodeLine(input);
            Assert.Equal(expected, actual);
        }
    }
}
