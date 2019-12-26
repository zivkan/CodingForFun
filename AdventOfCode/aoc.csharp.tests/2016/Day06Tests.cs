using aoc.csharp._2016;
using System.Collections.Generic;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day06Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2016, 06);
            var (part1, part2) = Day06.GetAnswer(input);

            Assert.Equal("bjosfbce", part1);
            Assert.Equal("veqfxzfx", part2);
        }

        private static readonly IReadOnlyList<string> SampleInput = new List<string>
        {
            "eedadn",
            "drvtee",
            "eandsr",
            "raavrd",
            "atevrs",
            "tsrnev",
            "sdttsa",
            "rasrtv",
            "nssdts",
            "ntnada",
            "svetve",
            "tesnvt",
            "vntsnd",
            "vrdear",
            "dvrsen",
            "enarar"
        };

        [Fact]
        public void Part1Sample()
        {
            var code = Day06.GetMostRepeatedCode(SampleInput);
            Assert.Equal("easter", code);
        }

        [Fact]
        public void Part2Sample()
        {
            var code = Day06.GetLeastRepeatedCode(SampleInput);
            Assert.Equal("advent", code);
        }
    }
}
