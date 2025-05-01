using aoc.csharp._2015;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day06Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2015, 06);
            var (part1, part2) = Day06.GetAnswer(input);

            Assert.Equal("400410", part1);
            Assert.Equal("15343601", part2);
        }
    }
}
