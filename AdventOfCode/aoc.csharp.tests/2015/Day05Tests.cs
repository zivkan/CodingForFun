using aoc.csharp._2015;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day05Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 05);
            var (part1, part2) = Day05.GetAnswer(input);

            Assert.Equal("236", part1);
            Assert.Equal("51", part2);
        }

        [Theory]
        [InlineData("ugknbfddgicrmopn", true)]
        [InlineData("aaa", true)]
        [InlineData("jchzalrnumimnmhp", false)]
        [InlineData("haegwjzuvuyypxyu", false)]
        [InlineData("dvszwmarrgswjxmb", false)]
        public void Part1Sample(string input, bool expected)
        {
            bool actual = Day05.Part1(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("qjhvhtzxzqqjkmpb", true)]
        [InlineData("xxyxx", true)]
        [InlineData("uurcxstgmygtbstg", false)]
        [InlineData("ieodomkazucvgmuy", false)]
        public void Part2Sample(string input, bool expected)
        {
            bool actual = Day05.Part2(input);
            Assert.Equal(expected, actual);
        }
    }
}
