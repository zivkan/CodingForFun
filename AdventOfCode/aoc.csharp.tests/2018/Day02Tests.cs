using aoc.csharp._2018;
using Xunit;

namespace aoc.csharp.tests._2018
{
    public class Day02Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2018, 09);
            var (part1, part2) = Day09.GetAnswer(input);

            Assert.Equal("413188", part1);
            Assert.Equal("3377272893", part2);
        }

        [Fact]
        public void Part1Sample()
        {
            string input =
                "abcdef\n" +
                "bababc\n" +
                "abbcde\n" +
                "abcccd\n" +
                "aabcdd\n" +
                "abcdee\n" +
                "ababab";
            int result = Day02.CalculateChecksum(input);
            Assert.Equal(12, result);
        }

        [Fact]
        public void Part2Sample()
        {
            string input =
                "abcde\n" +
                "fghij\n" +
                "klmno\n" +
                "pqrst\n" +
                "fguij\n" +
                "axcye\n" +
                "wvxyz";
            string result = Day02.FindCorrectBoxCommonChars(input);
            Assert.Equal("fgij", result);
        }
    }
}
