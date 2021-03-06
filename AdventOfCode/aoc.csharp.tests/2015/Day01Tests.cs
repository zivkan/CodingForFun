using aoc.csharp._2015;
using System.IO;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day01Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 01);
            var (part1, part2) = Day01.GetAnswer(input);

            Assert.Equal("280", part1);
            Assert.Equal("1797", part2);
        }

        [Theory]
        [InlineData("(())", 0)]
        [InlineData("()()", 0)]
        [InlineData("(((", 3)]
        [InlineData("(()(()(", 3)]
        [InlineData("))(((((", 3)]
        [InlineData("())", -1)]
        [InlineData("))(", -1)]
        [InlineData(")))", -3)]
        [InlineData(")())())", -3)]
        public void Part1Sample(string input, int expected)
        {
            string actual;
            using (var reader = new StringReader(input))
            {
                (actual, _) = Day01.GetAnswer(reader);
            }
            Assert.Equal(expected.ToString(), actual);
        }

        [Theory]
        [InlineData(")", 1)]
        [InlineData("()())", 5)]
        public void Part2Sample(string input, int expected)
        {
            string actual;
            using (var reader = new StringReader(input))
            {
                (_, actual) = Day01.GetAnswer(reader);
            }
            Assert.Equal(expected.ToString(), actual);
        }
    }
}
