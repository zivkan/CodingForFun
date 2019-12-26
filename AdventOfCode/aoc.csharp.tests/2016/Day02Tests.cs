using aoc.csharp._2016;
using System.Collections.Generic;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day02Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2016, 02);
            var (part1, part2) = Day02.GetAnswer(input);

            Assert.Equal("65556", part1);
            Assert.Equal("CB779", part2);
        }

        private static readonly IReadOnlyList<string> SampleInput = new List<string>()
        {
            "ULL",
            "RRDDD",
            "LURDL",
            "UUUUD"
        };

        [Fact]
        public void Part1Sample()
        {
            string expected = "1985";

            var code = Day02.GetCodeWithKeypad1(SampleInput);

            Assert.Equal(expected, code);
        }

        [Fact]
        public void Part2Sample()
        {
            string expected = "5DB3";

            var code = Day02.GetCodeWithKeypad2(SampleInput);

            Assert.Equal(expected, code);
        }
    }
}
