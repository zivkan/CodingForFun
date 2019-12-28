using aoc.csharp._2018;
using Xunit;

namespace aoc.csharp.tests._2018
{
    public class Day05Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2018, 05);
            var (part1, part2) = Day05.GetAnswer(input);

            Assert.Equal("11476", part1);
            Assert.Equal("5446", part2);
        }

        private static readonly string _sampleInput = "dabAcCaCBAcCcaDA";

        [Fact]
        public void Part1Sample()
        {
            var result = Day05.React(_sampleInput);
            Assert.Equal(10, result.Length);
        }

        [Fact]
        public void Part2Sample()
        {
            var result = Day05.BestReaction(_sampleInput);
            Assert.Equal(4, result.Length);
        }
    }
}
