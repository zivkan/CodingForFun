using aoc.csharp._2016;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2016
{
    public class Day02Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2016, 02);
            var (part1, part2) = Day02.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
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
