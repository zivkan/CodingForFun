using aoc.csharp._2017;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017
{
    public class Day16Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 16);
            var (part1, part2) = Day16.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Fact]
        public void Sample()
        {
            var programs = Enumerable.Range(0, 5).ToArray();
            var dance = Day16.Parse("s1,x3/4,pe/b", programs.Length);
            Day16.RunDance(programs, dance);
            var result = Day16.ToString(programs);
            Assert.Equal("baedc", result);
        }
    }
}
