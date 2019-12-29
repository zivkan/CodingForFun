using System.Linq;
using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day16Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 16);
            var (part1, part2) = Day16.GetAnswer(input);

            Assert.Equal("bkgcdefiholnpmja", part1);
            Assert.Equal("knmdfoijcbpghlea", part2);
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
