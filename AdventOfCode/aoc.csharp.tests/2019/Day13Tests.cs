using aoc.csharp._2019;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day13Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2019, 13);
            var (part1, part2) = Day13.GetAnswer(input);

            Assert.Equal("216", part1);
            Assert.Equal("10025", part2);
        }
    }
}
