using aoc.csharp._2019;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2019
{
    public class Day01Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2019, 01);
            var (part1, part2) = Day01.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void GetRequiredFuelTests(int mass, int expected)
        {
            var actual = Day01.GetRequiredFuel(mass);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void GetTotalFuelTests(int mass, int expected)
        {
            var actual = Day01.GetTotalFuel(mass);
            Assert.Equal(expected, actual);
        }
    }
}
