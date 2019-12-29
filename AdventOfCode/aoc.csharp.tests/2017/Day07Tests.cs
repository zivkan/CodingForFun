using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day07Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 07);
            var (part1, part2) = Day07.GetAnswer(input);

            Assert.Equal("xegshds", part1);
            Assert.Equal("299", part2);
        }

        private const string _sample = @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";

        [Fact]
        public void Part1Sample()
        {
            var tree = Day07.BuildTree(_sample);
            Assert.Equal("tknk", tree.Name);
        }

        [Fact]
        public void Part2Sample()
        {
            var tree = Day07.BuildTree(_sample);
            var weightDiff = Day07.GetWeightDifference(tree);
            Assert.Equal(60, weightDiff);
        }
    }
}
