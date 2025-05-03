using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017
{
    public class Day20Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 20);
            var (part1, part2) = Day20.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Fact]
        public void Part1Sample()
        {
            const string input =
                "p=<3,0,0>, v=<2,0,0>, a=<-1,0,0>\n" +
                "p=<4,0,0>, v=<0,0,0>, a=<-2,0,0>";

            var actual = Day20.FindParticleThatStaysClosestToOrigin(input);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void Part2Sample()
        {
            const string input =
                "p=<-6,0,0>, v=<3,0,0>, a=<0,0,0>\n" +
                "p=<-4,0,0>, v=<2,0,0>, a=<0,0,0>\n" +
                "p=<-2,0,0>, v=<1,0,0>, a=<0,0,0>\n" +
                "p=<3,0,0>, v=<-1,0,0>, a=<0,0,0>";

            var actual = Day20.FindParticleCountAfterCollisions(input);
            Assert.Equal(1, actual);
        }
    }
}
