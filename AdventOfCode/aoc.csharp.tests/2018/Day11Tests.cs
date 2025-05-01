using aoc.csharp._2018;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2018
{
    public class Day11Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2018, 11);
            var (part1, part2) = Day11.GetAnswer(input);

            Assert.Equal("20,54", part1);
            Assert.Equal("233,93,13", part2);
        }

        [Theory]
        [InlineData(3, 5, 8, 4)]
        [InlineData(122, 79, 57, -5)]
        [InlineData(217, 196, 39, 0)]
        [InlineData(101, 153, 71, 4)]
        public void TestPowerLevel(int x, int y, int serial, int expected)
        {
            var actual = Day11.GetPowerLevel(x, y, serial);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(18, "33,45", 29)]
        [InlineData(42, "21,61", 30)]
        public void Part1Sample(int serial, string expected, int expectedPower)
        {
            var (result, power) = Day11.FindHighest3x3PowerRegion(serial);
            Assert.Equal(expectedPower, power);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(18, "90,269,16", 113)]
        [InlineData(42, "232,251,12", 119)]
        public void Part2Sample(int serial, string expected, int expectedPower)
        {
            var (result, power) = Day11.FindHighestPowerRegion(serial);
            Assert.Equal(expectedPower, power);
            Assert.Equal(expected, result);
        }
    }
}
