using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2018
{
    public class Day01 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var part1 = AccumulateChanges(0, text).Last();
            var part2 = FirstReoccurance(text);
            return (part1.ToString(), part2.Value.ToString());
        }

        public static IEnumerable<int> AccumulateChanges(int initial, string input)
        {
            var freq = initial;
            using var reader = new StringReader(input);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var change = int.Parse(line);
                freq += change;
                yield return freq;
            }
        }

        public static int? FirstReoccurance(string input)
        {
            var seen = new HashSet<int>();
            int initial = 0;
            seen.Add(initial);
            // no infinite loop
            for (int i = 0; i < 1000; i++)
            {
                foreach (var freq in AccumulateChanges(initial, input))
                {
                    if (!seen.Add(freq))
                    {
                        return freq;
                    }
                    initial = freq;
                }
            }

            return null;
        }
    }
}
