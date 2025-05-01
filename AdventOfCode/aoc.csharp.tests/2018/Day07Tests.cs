using aoc.csharp._2018;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2018
{
    public class Day07Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2018, 07);
            var (part1, part2) = Day07.GetAnswer(input);

            Assert.Equal("CFMNLOAHRKPTWBJSYZVGUQXIDE", part1);
            Assert.Equal("971", part2);
        }

        private static readonly string _sampleInput =
            "Step C must be finished before step A can begin.\n" +
            "Step C must be finished before step F can begin.\n" +
            "Step A must be finished before step B can begin.\n" +
            "Step A must be finished before step D can begin.\n" +
            "Step B must be finished before step E can begin.\n" +
            "Step D must be finished before step E can begin.\n" +
            "Step F must be finished before step E can begin.";

        [Fact]
        public void Part1Sample()
        {
            var (order, _) = Day07.GetOrder(_sampleInput, 1, 0);
            Assert.Equal("CABDFE", order);
        }

        [Fact]
        public void Part2Sample()
        {
            var (order, elapsedTime) = Day07.GetOrder(_sampleInput, 2, 0);
            Assert.Equal("CABFDE", order);
            Assert.Equal(15, elapsedTime);
        }
    }
}
