using aoc.csharp._2016;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day05Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2016, 05);
            var (part1, part2) = Day05.GetAnswer(input);

            Assert.Equal("d4cd2ee1", part1);
            Assert.Equal("f2c730e5", part2);
        }

        [Fact]
        public void Part1Sample()
        {
            var doorId = "abc";
            var finalCode = Day05.GetPart1Code(doorId);
            Assert.Equal("18f47a30", finalCode);
        }

        [Fact]
        public void Part2Sample()
        {
            var doorId = "abc";
            var finalCode = Day05.GetPart2Code(doorId);
            Assert.Equal("05ace8e3", finalCode);
        }
    }
}
