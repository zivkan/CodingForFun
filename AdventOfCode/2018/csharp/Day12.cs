using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using input;
using Xunit;

namespace csharp
{
    public class Day12
    {
        private static readonly string _input = GetInput.Day(12);

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
            var (mapping, initialState) = ParseInput(input, true);

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

            var pots = initialState;
            var str = " 0: " + ToString(pots, -3, 35);
            Assert.Equal(expected[0], str);
            for (int generation = 1; generation <= 20; generation++)
            {
                pots = NextGeneration(pots, mapping);
                str = generation.ToString().PadLeft(2) + ": " + ToString(pots, -3, 35);
                Assert.Equal(expected[generation], str);
            }

            Assert.Equal(-2, pots.Min());
            Assert.Equal(325, pots.Sum());
        }

        [Fact]
        public void Part1()
        {
            var (mapping, initialState) = ParseInput(_input, false);

            var pots = initialState;
            for (int generation = 1; generation <= 20; generation++)
            {
                pots = NextGeneration(pots, mapping);
            }

            Assert.Equal(1991, pots.Sum());
        }

        [Fact]
        public void Part2()
        {
            long target = 50_000_000_000;
            var (mapping, pots) = ParseInput(_input, false);

            // start simulation, but detect loops
            var history = new Dictionary<string, (long generation, int left)>();
            var str = ToString(pots, pots[0], pots[pots.Count - 1]);
            history.Add(str, (0, pots[0]));
            long loopTarget = -1;
            long loopOffset = 0;
            for (long generation = 1; generation <= target; generation++)
            {
                var newPots = NextGeneration(pots, mapping);
                var newStr = ToString(newPots, newPots[0], newPots[newPots.Count - 1]);
                if (history.TryGetValue(newStr, out var value))
                {
                    loopTarget = value.generation;
                    loopOffset = newPots[0] - value.left;
                    break;
                }
                else
                {
                    history.Add(newStr, (generation, newPots[0]));
                }
                pots = newPots;
            }

            // now that we have a loop, we can re-order the data into a timeline
            var historyList = new (string pots, int left)[history.Count];
            foreach (var h in history)
            {
                historyList[h.Value.generation] = (h.Key, h.Value.left);
            }

            // calculate the number of loops, and which pots layout is equivilent to the target generation
            var loopLength = historyList.LongLength - loopTarget;
            var loops = (target - loopTarget) / loopLength;
            var remaining = target - loopTarget - loops * loopLength;
            var finalPosition = historyList[loopTarget + remaining];

            // Finally, calculate the left position offset of the target generation, and the sum
            var finalOffset = finalPosition.left + loops * loopOffset;
            str = finalPosition.pots;
            long sum = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '#')
                {
                    sum += i + finalOffset;
                }
            }

            Assert.Equal(1100000000511, sum);
        }

        private (bool[] mapping, List<int> initialState) ParseInput(string input, bool sample)
        {
            var initialState = new List<int>();
            var mapping = new bool[32];
            using (var reader = new StringReader(input))
            {
                string line = reader.ReadLine();
                if (!line.StartsWith("initial state: ")) throw new Exception();
                var initialStateData = line.AsSpan(15);
                for (int i = 0; i < initialStateData.Length; i++)
                {
                    if (initialStateData[i] == '#')
                    {
                        initialState.Add(i);
                    }
                }

                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line)) throw new Exception();

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length != 10 || line.Substring(5, 4) != " => ") throw new Exception();
                    if (line[9] == '#')
                    {
                        var index =
                            (line[0] == '#' ? 1 << 4 : 0) +
                            (line[1] == '#' ? 1 << 3 : 0) +
                            (line[2] == '#' ? 1 << 2 : 0) +
                            (line[3] == '#' ? 1 << 1 : 0) +
                            (line[4] == '#' ? 1 << 0 : 0);
                        mapping[index] = true;
                    }
                }
            }

            return (mapping, initialState);
        }

        private List<int> NextGeneration(List<int> pots, bool[] mapping)
        {
            Debug.Assert(mapping.Length == 32);

            var next = new List<int>();
            int left, right;
            if (pots.Count == 0)
            {
                left = 0;
                right = 0;
            }
            else
            {
                left = pots[0] - 2;
                right = pots[pots.Count - 1] + 2;
            }
            int cursor = 0;

            int number = 0;
            for (int position = left; position <= right; position++)
            {
                number = (number << 1) & 31;
                if (cursor < pots.Count && pots[cursor] == position + 2)
                {
                    number += 1;
                    cursor++;
                }
                if (mapping[number])
                {
                    next.Add(position);
                }
            }

            return next;
        }

        private string ToString(List<int> state, int left, int right)
        {
            var chars = new char[right - left + 1];
            Array.Fill(chars, '.');

            foreach (var i in state)
            {
                chars[i - left] = '#';
            }

            return new string(chars);
        }
    }
}
