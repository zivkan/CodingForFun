using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day14Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 14);
            var (part1, part2) = Day14.GetAnswer(input);

            Assert.Equal("8074", part1);
            Assert.Equal("1212", part2);
        }

        [Fact]
        public void Sample()
        {
            string input = "flqrgnkx";
            var used = Day14.GetUsed(input);
            Assert.Equal(8108, used.Count);

            var groups = Day14.Group(used);
            Assert.Equal(1242, groups.Count);
        }
    }
}
