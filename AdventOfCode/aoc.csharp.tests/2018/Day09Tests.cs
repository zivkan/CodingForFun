using aoc.csharp._2018;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2018
{
    public class Day09Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2018, 09);
            var (part1, part2) = Day09.GetAnswer(input);

            Assert.Equal("413188", part1);
            Assert.Equal("3377272893", part2);
        }

        [Theory]
        [InlineData("9 players; last marble is worth 25 points", 32)]
        [InlineData("10 players; last marble is worth 1618 points", 8317)]
        [InlineData("13 players; last marble is worth 7999 points", 146373)]
        [InlineData("17 players; last marble is worth 1104 points", 2764)]
        [InlineData("21 players; last marble is worth 6111 points", 54718)]
        [InlineData("30 players; last marble is worth 5807 points", 37305)]
        public void Part1Sample(string input, int expected)
        {
            var (players, maxPoints) = Day09.ParseInput(input);
            var result = Day09.PlayGame(players, maxPoints);
            Assert.Equal(expected, result);
        }
    }
}
