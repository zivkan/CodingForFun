using System.Linq;
using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day12Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 12);
            var (part1, part2) = Day12.GetAnswer(input);

            Assert.Equal("175", part1);
            Assert.Equal("213", part2);
        }

        private const string _sample = @"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5";

        [Fact]
        public void Sample()
        {
            var groups = Day12.GroupPrograms(_sample);
            Assert.Equal(6, groups.Single(g=>g.Contains(0)).Count);
            Assert.Equal(2, groups.Count);
        }
    }
}
