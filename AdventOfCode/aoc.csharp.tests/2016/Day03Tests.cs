using aoc.csharp._2016;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day03Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2016, 03);
            var (part1, part2) = Day03.GetAnswer(input);

            Assert.Equal("917", part1);
            Assert.Equal("1649", part2);
        }
    }
}
