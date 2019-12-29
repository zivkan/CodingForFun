using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2017
{
    public class Day05 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var (part1, _) = CountSteps(text, Part1Increment);
            var (part2, _) = CountSteps(text, Part2Increment);
            return (part1.ToString(), part2.ToString());
        }

        public static (int steps, List<int> offsets) CountSteps(string input, Func<int, int> newValue)
        {
            List<int> offsets = new List<int>();
            using (var reader = new StringReader(input))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    int value = int.Parse(line);
                    offsets.Add(value);
                }
            }

            int step = 0;
            int position = 0;

            while (position >= 0 && position < offsets.Count)
            {
                step++;
                int offset = offsets[position];
                offsets[position] = newValue(offset);
                position += offset;
            }

            return (step, offsets);
        }

        public static int Part1Increment(int value)
        {
            return value + 1;
        }

        public static int Part2Increment(int value)
        {
            return value >= 3
                ? value - 1
                : value + 1;
        }
    }
}
