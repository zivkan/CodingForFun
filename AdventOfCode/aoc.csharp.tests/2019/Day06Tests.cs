using aoc.csharp._2019;
using System.IO;
using System.Linq;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day06Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2019, 06);
            var (part1, part2) = Day06.GetAnswer(input);

            Assert.Equal("315757", part1);
            Assert.Equal("481", part2);
        }

        [Fact]
        public void Part1Test()
        {
            string input = @"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L";
            using var reader = new StringReader(input);
            var result = Day06.ParseGraph(reader);

            Assert.Equal(42, result.Select(kvp => kvp.Value.Depth).Sum());
        }

        [Fact]
        public void Part2Test()
        {
            string input = @"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L
K)YOU
I)SAN";
            using var reader = new StringReader(input);
            (_, var part2) = Day06.GetAnswer(reader);

            Assert.Equal("4", part2);
        }
    }
}
