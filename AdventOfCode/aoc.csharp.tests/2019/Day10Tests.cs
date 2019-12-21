using aoc.csharp._2019;
using aoc.csharp.Geometry;
using System.Linq;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day10Tests
    {
        private static readonly string[] _sampleMap = new string[]
        {
                ".#..#",
                ".....",
                "#####",
                "....#",
                "...##"
        };

        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2019, 10);
            var (part1, part2) = Day10.GetAnswer(input);

            Assert.Equal("292", part1);
            Assert.Equal("317", part2);
        }

        [Theory]
        [InlineData(1, 0, 1, 0)]
        [InlineData(0, 1, 0, 1)]
        [InlineData(-1, 0, -1, 0)]
        [InlineData(0, -1, 0, -1)]
        [InlineData(1, 1, 1, 1)]
        [InlineData(5, 1, 5, 1)]
        [InlineData(5, 0, 1, 0)]
        [InlineData(2, 2, 1, 1)]
        [InlineData(6, 2, 3, 1)]
        public void GetDirectionTests(int x, int y, int expectedX, int expectedY)
        {
            var vector = new Point2D(x, y);
            var expected = new Point2D(expectedX, expectedY);
            var actual = Day10.GetDirection(vector);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0, 7)]
        [InlineData(4, 0, 7)]
        [InlineData(0, 2, 6)]
        [InlineData(1, 2, 7)]
        [InlineData(2, 2, 7)]
        [InlineData(3, 2, 7)]
        [InlineData(4, 2, 5)]
        [InlineData(4, 3, 7)]
        [InlineData(3, 4, 8)]
        [InlineData(4, 4, 7)]
        public void VisibleAstroidsTests(int x, int y, int expected)
        {
            var asteroids = Day10.GetAsteroids(_sampleMap);
            var asteroidInfos = Day10.GetAsteroidInfos(asteroids, new Point2D(x, y));
            var visible = Day10.GetVisibleAsteroidCount(asteroidInfos);

            Assert.Equal(expected, visible);
        }

        [Fact]
        public void GetLaserTargetsTest()
        {
            var map = new string[]
            {
                ".#....#####...#..",
                "##...##.#####..##",
                "##...#...#.#####.",
                "..#.....X...###..",
                "..#.#.....#....##"
            };
            var expectedOrder = new string[]
            {
                ".Z....cdeBD...g..",
                "WX...ab.ACfFG..Ih",
                "UV...Y...E.HJKLM.",
                "..T.....X...Nij..",
                "..S.R.....Q....PO"
            };
            var asteroids = Day10.GetAsteroids(map);
            var targets = Day10.GetAsteroidInfos(asteroids, new Point2D(8, 3));
            var order = Day10.GetLaserTargets(targets).ToList();

            var expected = targets.Select(t => t.Position)
                .OrderBy(a => expectedOrder[a.Y][a.X])
                .ToList();

            Assert.Equal(expected.Count, order.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], order[i]);
            }
        }
    }
}
