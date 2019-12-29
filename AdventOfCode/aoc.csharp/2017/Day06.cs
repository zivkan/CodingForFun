using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2017
{
    public class Day06 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var (part1, part2, _) = IterateUntilRepeat(text);
            return (part1.ToString(), part2.ToString());
        }

        public static (int cycles, int loopLength, int[] banks) IterateUntilRepeat(string input)
        {
            int cycles = 0;
            var banks = input.Split('\t').Select(int.Parse).ToArray();
            int count = banks.Length;

            List<int[]> history = new List<int[]>
            {
                banks
            };

            while (true && cycles < 1000000)
            {
                cycles++;
                int max = banks[0];
                int chosenBank = 0;
                for (int i = 0; i < count; i++)
                {
                    if (banks[i] > max)
                    {
                        max = banks[i];
                        chosenBank = i;
                    }
                }

                int[] newBanks = (int[])banks.Clone();
                newBanks[chosenBank] = 0;
                for (int i = 0; i < max; i++)
                {
                    chosenBank = (chosenBank + 1) % count;
                    newBanks[chosenBank]++;
                }

                for (var j = 0; j < history.Count; j++)
                {
                    var oldBank = history[j];
                    bool same = true;
                    for (int i = 0; i < count; i++)
                    {
                        if (oldBank[i] != newBanks[i])
                        {
                            same = false;
                            break;
                        }
                    }

                    if (same)
                    {
                        int loopLength = history.Count - j;
                        return (cycles, loopLength, newBanks);
                    }
                }

                history.Add(newBanks);
                banks = newBanks;
            }

            throw new Exception();
        }
    }
}
