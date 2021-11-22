using aoc.csharp._2015;
using System.IO;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day09Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 09);
            var (part1, part2) = Day09.GetAnswer(input);

            Assert.Equal("251", part1);
            Assert.Equal("898", part2);
        }

        [Fact]
        public void Tests()
        {
            var input = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";

            string min, max;
            using (var reader = new StringReader(input))
            {
                (min, max) = Day09.GetAnswer(reader);
            }

            Assert.Equal("605", min);
            Assert.Equal("982", max);
        }
    }
}
