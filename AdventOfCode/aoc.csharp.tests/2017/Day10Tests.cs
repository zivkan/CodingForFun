using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017
{
    public class Day10Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 10);
            var (part1, part2) = Day10.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        private const string _sample = "3,4,1,5";

        [Fact]
        public void Part1Sample()
        {
            var list = Day10.GetResult(5, _sample);
            int result = list[0] * list[1];
            Assert.Equal(12, result);
        }

        [Theory]
        [InlineData("", "a2582a3a0e66e6e86e3812dcb672a272")]
        [InlineData("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd")]
        [InlineData("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d")]
        [InlineData("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e")]
        public void Part2Sample(string input, string expected)
        {
            string hash = Day10.KnotHash(input);
            Assert.Equal(expected, hash);
        }
    }
}
