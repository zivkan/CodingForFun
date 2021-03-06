using aoc.csharp._2018;
using System;
using System.Text;
using Xunit;

namespace aoc.csharp.tests._2018
{
    public class Day10Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2018, 10);
            var (part1, part2) = Day10.GetAnswer(input);

            var expectedPart1 =
                "#####.....##....#....#..#.......#####.....##....#####...#####." + Environment.NewLine +
                "#....#...#..#...##...#..#.......#....#...#..#...#....#..#....#" + Environment.NewLine +
                "#....#..#....#..##...#..#.......#....#..#....#..#....#..#....#" + Environment.NewLine +
                "#....#..#....#..#.#..#..#.......#....#..#....#..#....#..#....#" + Environment.NewLine +
                "#####...#....#..#.#..#..#.......#####...#....#..#####...#####." + Environment.NewLine +
                "#.......######..#..#.#..#.......#.......######..#.......#..#.." + Environment.NewLine +
                "#.......#....#..#..#.#..#.......#.......#....#..#.......#...#." + Environment.NewLine +
                "#.......#....#..#...##..#.......#.......#....#..#.......#...#." + Environment.NewLine +
                "#.......#....#..#...##..#.......#.......#....#..#.......#....#" + Environment.NewLine +
                "#.......#....#..#....#..######..#.......#....#..#.......#....#" + Environment.NewLine;

            Assert.Equal(expectedPart1, part1);
            Assert.Equal("10304", part2);
        }

        private static readonly string _sampleInput = @"position=< 9,  1> velocity=< 0,  2>
position=< 7,  0> velocity=<-1,  0>
position=< 3, -2> velocity=<-1,  1>
position=< 6, 10> velocity=<-2, -1>
position=< 2, -4> velocity=< 2,  2>
position=<-6, 10> velocity=< 2, -2>
position=< 1,  8> velocity=< 1, -1>
position=< 1,  7> velocity=< 1,  0>
position=<-3, 11> velocity=< 1, -2>
position=< 7,  6> velocity=<-1, -1>
position=<-2,  3> velocity=< 1,  0>
position=<-4,  3> velocity=< 2,  0>
position=<10, -3> velocity=<-1,  1>
position=< 5, 11> velocity=< 1, -2>
position=< 4,  7> velocity=< 0, -1>
position=< 8, -2> velocity=< 0,  1>
position=<15,  0> velocity=<-2,  0>
position=< 1,  6> velocity=< 1,  0>
position=< 8,  9> velocity=< 0, -1>
position=< 3,  3> velocity=<-1,  1>
position=< 0,  5> velocity=< 0, -1>
position=<-2,  2> velocity=< 2,  0>
position=< 5, -2> velocity=< 1,  2>
position=< 1,  4> velocity=< 2,  1>
position=<-2,  7> velocity=< 2, -2>
position=< 3,  6> velocity=<-1, -1>
position=< 5,  0> velocity=< 1,  0>
position=<-6,  0> velocity=< 2,  0>
position=< 5,  9> velocity=< 1, -2>
position=<14,  7> velocity=<-2,  0>
position=<-3,  6> velocity=< 2, -1>";

        [Fact]
        public void Part1Sample()
        {
            var (message, seconds) = Day10.FindMessage(_sampleInput);
            var expected = new StringBuilder();
            expected.AppendLine("#...#..###");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#####...#.");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#...#..###");
            Assert.Equal(expected.ToString(), message);
            Assert.Equal(3, seconds);
        }
    }
}
