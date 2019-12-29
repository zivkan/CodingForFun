using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day15Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 15);
            var (part1, part2) = Day15.GetAnswer(input);

            Assert.Equal("594", part1);
            Assert.Equal("328", part2);
        }

        [Fact]
        public void Sample()
        {
            var matches = Day15.CountMatches(Day15.Generator(65, Day15.GeneratorAMultiplier, 1),
                Day15.Generator(8921, Day15.GeneratorBMultiplier, 1),
                40_000_000);
            Assert.Equal(588, matches);

            matches = Day15.CountMatches(Day15.Generator(65, Day15.GeneratorAMultiplier, 4),
                Day15.Generator(8921, Day15.GeneratorBMultiplier, 8),
                5_000_000);
            Assert.Equal(309, matches);
        }
    }
}
