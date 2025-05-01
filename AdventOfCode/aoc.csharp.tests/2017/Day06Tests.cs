using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day06Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 06);
            var (part1, part2) = Day06.GetAnswer(input);

            Assert.Equal("5042", part1);
            Assert.Equal("1086", part2);
        }

        [Fact]
        public void Part1Sample()
        {
            const string input = "0\t2\t7\t0";
            var (cycles, loopLength, banks) = Day06.IterateUntilRepeat(input);
            Assert.Equal(5, cycles);
            Assert.Equal(banks, new[] { 2, 4, 1, 2 });
            Assert.Equal(4, loopLength);
        }
    }
}
