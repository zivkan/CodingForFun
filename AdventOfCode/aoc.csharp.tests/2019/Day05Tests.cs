using aoc.csharp._2019;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day05Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2019, 05);
            var (part1, part2) = Day05.GetAnswer(input);

            Assert.Equal("16225258", part1);
            Assert.Equal("2808771", part2);
        }
    }
}
