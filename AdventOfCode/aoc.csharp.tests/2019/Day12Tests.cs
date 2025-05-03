using aoc.csharp._2019;
using aoc.csharp.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2019
{
    public class Day12Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2019, 12);
            var (part1, part2) = Day12.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Fact]
        public void ParseTest()
        {
            var text = "<x=-1, y=0, z=2>" + Environment.NewLine +
                "<x=2, y=-10, z=-7>" + Environment.NewLine +
                "<x=4, y=-8, z=8>" + Environment.NewLine +
                "<x=3, y=5, z=-1>";

            Day12.Moons moons;
            using (var reader = new StringReader(text))
            {
                moons = Day12.ParseInitialState(reader);
            }

            Assert.Equal(4, moons.Count);
            Assert.Equal(new Point3D(-1, 0, 2), moons[0].Position);
            Assert.Equal(Point3D.Zero, moons[0].Velocity);
            Assert.Equal(new Point3D(2, -10, -7), moons[1].Position);
            Assert.Equal(Point3D.Zero, moons[1].Velocity);
            Assert.Equal(new Point3D(4, -8, 8), moons[2].Position);
            Assert.Equal(Point3D.Zero, moons[2].Velocity);
            Assert.Equal(new Point3D(3, 5, -1), moons[3].Position);
            Assert.Equal(Point3D.Zero, moons[3].Velocity);
        }

        [Fact]
        public void StepTest()
        {
            Day12.Moons moons = new Day12.Moons(new List<Day12.Moon>(4)
            {
                new Day12.Moon(new Point3D(-1, 0, 2), Point3D.Zero),
                new Day12.Moon(new Point3D(2, -10, -7), Point3D.Zero),
                new Day12.Moon(new Point3D(4, -8, 8), Point3D.Zero),
                new Day12.Moon(new Point3D(3, 5, -1), Point3D.Zero)
            });

            var result = Day12.Step(moons);

            Assert.Equal(moons.Count, result.Count);
            Assert.Equal(new Point3D(2, -1, 1), result[0].Position);
            Assert.Equal(new Point3D(3, -1, -1), result[0].Velocity);
            Assert.Equal(new Point3D(3, -7, -4), result[1].Position);
            Assert.Equal(new Point3D(1, 3, 3), result[1].Velocity);
            Assert.Equal(new Point3D(1, -7, 5), result[2].Position);
            Assert.Equal(new Point3D(-3, 1, -3), result[2].Velocity);
            Assert.Equal(new Point3D(2, 2, 0), result[3].Position);
            Assert.Equal(new Point3D(-1, -3, 1), result[3].Velocity);
        }

        [Fact]
        public void GetEnergyTest()
        {
            Day12.Moons moons = new Day12.Moons(new List<Day12.Moon>(4)
            {
                new Day12.Moon(new Point3D(2, 1, -3), new Point3D(-3, -2, 1)),
                new Day12.Moon(new Point3D(1, -8, 0), new Point3D(-1, 1, 3)),
                new Day12.Moon(new Point3D(3, -6, 1), new Point3D(3, 2, -3)),
                new Day12.Moon(new Point3D(2, 0, 4), new Point3D(1, -1, -1))
            });

            var energy = Day12.GetEnergy(moons);

            Assert.Equal(179, energy);
        }

        [Fact]
        public void GetCycleLengthTest()
        {
            var moons = new Day12.Moons(new List<Day12.Moon>(4)
            {
                new Day12.Moon(new Point3D(-1, 0, 2), Point3D.Zero),
                new Day12.Moon(new Point3D(2, -10, -7), Point3D.Zero),
                new Day12.Moon(new Point3D(4, -8, 8), Point3D.Zero),
                new Day12.Moon(new Point3D(3, 5, -1), Point3D.Zero)
            });

            var cycleLength = Day12.GetCycleLength(moons);

            Assert.Equal(2772, cycleLength);
        }
    }
}
