using aoc.csharp._2019;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day04Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2019, 04);
            var (part1, part2) = Day04.GetAnswer(input);

            Assert.Equal("2814", part1);
            Assert.Equal("1991", part2);
        }
    }
}
