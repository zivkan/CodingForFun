using aoc.csharp._2019;
using aoc.csharp.Geometry;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2019
{
    public class Day03Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2019, 03);
            var (part1, part2) = Day03.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Fact]
        public void GetIntersections()
        {
            var line1 = Day03.GetLinePoints("R8,U5,L5,D3");
            var line2 = Day03.GetLinePoints("U7,R6,D4,L4");

            var intersection = line1.Keys.Intersect(line2.Keys).ToList();

            Assert.Equal(2, intersection.Count);
            Assert.Contains(new Point2D(3, 3), intersection);
            Assert.Contains(new Point2D(6, 5), intersection);
        }

        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83",159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7",135)]
        public void ShortestManhattenDistance(string line1, string line2, uint expected)
        {
            var points1 = Day03.GetLinePoints(line1);
            var points2 = Day03.GetLinePoints(line2);

            var intersection = points1.Keys.Intersect(points2.Keys);
            var actual = Day03.ShortestManhattenDistance(intersection);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCrossedCircuitLength()
        {
            var line1 = Day03.GetLinePoints("R8,U5,L5,D3");
            var line2 = Day03.GetLinePoints("U7,R6,D4,L4");

            var intersection = line1.Keys.Intersect(line2.Keys);

            var result = Day03.GetCrossedCircuitLength(intersection, line1, line2);

            Assert.Equal(30u, result);
        }
    }
}
