using aoc.csharp._2019;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2019
{
    public class Day01Tests
    {
        ITestOutputHelper _output;

        public Day01Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Answer()
        {
            using (var input = Input.Get(2019, 01))
            {
                var result = Day01.GetAnswer(input);

                Assert.Equal("3465154", result.Part1);
                Assert.Equal("5194864", result.Part2);
            }
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
