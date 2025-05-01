using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day02Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 02);
            var (part1, part2) = Day02.GetAnswer(input);

            Assert.Equal("45158", part1);
            Assert.Equal("294", part2);
        }

        [Fact]
        public void Part1Sample()
        {
            string input = "5\t1\t9\t5\n7\t5\t3\n2\t4\t6\t8";
            int actual = Day02.CalcPart1(input);
            Assert.Equal(18, actual);
        }

        [Fact]
        public void Part2Sample()
        {
            string input = "5\t9\t2\t8\n9\t4\t7\t3\n3\t8\t6\t5";
            int checksum = Day02.CalcPart2(input);
            Assert.Equal(9, checksum);
        }
    }
}
