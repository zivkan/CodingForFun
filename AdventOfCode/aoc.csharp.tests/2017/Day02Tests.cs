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
            int[][] input = 
                [ 
                    [5, 1, 9, 5],
                    [7, 5, 3],
                    [2, 4, 6, 8]
                ];
            int actual = Day02.CalcPart1(input);
            Assert.Equal(18, actual);
        }

        [Fact]
        public void Part2Sample()
        {
            int[][] input =
                [
                    [ 5, 9, 2, 8 ],
                    [ 9, 4, 7, 3 ],
                    [ 3, 8, 6, 5 ]
                ];
            int checksum = Day02.CalcPart2(input);
            Assert.Equal(9, checksum);
        }
    }
}
