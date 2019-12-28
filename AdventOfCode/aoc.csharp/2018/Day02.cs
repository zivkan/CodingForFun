using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2018
{
    public class Day02 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var part1 = CalculateChecksum(text);
            var part2 = FindCorrectBoxCommonChars(text);
            return (part1.ToString(), part2);
        }

        public static int CalculateChecksum(string input)
        {
            var result = ReadLines(input)
                .Select(ConvertToCharCounts)
                .Select(i => new { Two = i.Count(j => j.Value == 2), Three = i.Count(j => j.Value == 3) })
                .Aggregate(new { Two = 0, Three = 0 }, (a, o) => new { Two = a.Two + (o.Two > 0 ? 1 : 0), Three = a.Three + (o.Three > 0 ? 1 : 0) });
            return result.Two * result.Three;
        }

        private static IEnumerable<string> ReadLines(string input)
        {
            using var reader = new StringReader(input);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        private static Dictionary<char, int> ConvertToCharCounts(string line)
        {
            return line.GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public static string FindCorrectBoxCommonChars(string input)
        {
            var lines = ReadLines(input).ToList();

            for (int i = 0; i < lines.Count - 1; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    var differences = CountDifferences(lines[i], lines[j]);
                    if (differences == 1)
                    {
                        int index = FindIndexOfDifference(lines[i], lines[j]);
                        return lines[i].Remove(index, 1);
                    }
                }
            }

            throw new Exception();
        }

        private static int CountDifferences(string v1, string v2)
        {
            if (v1.Length != v2.Length) throw new InvalidOperationException();

            int differences = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                if (v1[i] != v2[i])
                {
                    differences++;
                }
            }

            return differences;
        }

        private static int FindIndexOfDifference(string v1, string v2)
        {
            for (int i = 0; i < v1.Length; i++)
            {
                if (v1[i] != v2[i]) return i;
            }

            throw new ArgumentException();
        }
    }
}
