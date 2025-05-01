using aoc.csharp._2018;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2018
{
    public class Day12Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2018, 12);
            var (part1, part2) = Day12.GetAnswer(input);

            Assert.Equal("1991", part1);
            Assert.Equal("1100000000511", part2);
        }

        [Fact]
        public void Part1Sample()
        {
            const string input = @"initial state: #..#.#..##......###...###

...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #";
            bool[] mapping;
            IReadOnlyList<int> initialState;
            using (var reader = new StringReader(input))
            {
                (mapping, initialState) = Day12.ParseInput(reader);
            }

            var expected = new[]
            {
                " 0: ...#..#.#..##......###...###...........",
                " 1: ...#...#....#.....#..#..#..#...........",
                " 2: ...##..##...##....#..#..#..##..........",
                " 3: ..#.#...#..#.#....#..#..#...#..........",
                " 4: ...#.#..#...#.#...#..#..##..##.........",
                " 5: ....#...##...#.#..#..#...#...#.........",
                " 6: ....##.#.#....#...#..##..##..##........",
                " 7: ...#..###.#...##..#...#...#...#........",
                " 8: ...#....##.#.#.#..##..##..##..##.......",
                " 9: ...##..#..#####....#...#...#...#.......",
                "10: ..#.#..#...#.##....##..##..##..##......",
                "11: ...#...##...#.#...#.#...#...#...#......",
                "12: ...##.#.#....#.#...#.#..##..##..##.....",
                "13: ..#..###.#....#.#...#....#...#...#.....",
                "14: ..#....##.#....#.#..##...##..##..##....",
                "15: ..##..#..#.#....#....#..#.#...#...#....",
                "16: .#.#..#...#.#...##...#...#.#..##..##...",
                "17: ..#...##...#.#.#.#...##...#....#...#...",
                "18: ..##.#.#....#####.#.#.#...##...##..##..",
                "19: .#..###.#..#.#.#######.#.#.#..#.#...#..",
                "20: .#....##....#####...#######....#.#..##."
            };

            var pots = initialState.ToList();
            var str = " 0: " + Day12.ToString(pots, -3, 35);
            Assert.Equal(expected[0], str);
            for (int generation = 1; generation <= 20; generation++)
            {
                pots = Day12.NextGeneration(pots, mapping);
                str = generation.ToString().PadLeft(2) + ": " + Day12.ToString(pots, -3, 35);
                Assert.Equal(expected[generation], str);
            }

            Assert.Equal(-2, pots.Min());
            Assert.Equal(325, pots.Sum());
        }
    }
}
