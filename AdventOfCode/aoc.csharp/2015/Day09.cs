using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2015
{
    public class Day09 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var map = Parse(input);
            var keys = map.Keys.ToList();
            int minDistance;
            int maxDistance;

            using (var enumerator = Permutations.Generate(keys).GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new Exception();
                }

                minDistance = maxDistance = CalculateDistance(enumerator.Current, map);

                while (enumerator.MoveNext())
                {
                    var distance = CalculateDistance(enumerator.Current, map);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                    }
                }
            }

            return (minDistance.ToString(), maxDistance.ToString());
        }

        private static int CalculateDistance(IReadOnlyList<string> directions, Dictionary<string, Dictionary<string, int>> map)
        {
            int totalDistance = 0;

            for (int i = 1; i < directions.Count; i++)
            {
                var destinations = map[directions[i - 1]];
                var distance = destinations[directions[i]];
                totalDistance += distance;
            }

            return totalDistance;
        }

        public static Dictionary<string, Dictionary<string, int>> Parse(TextReader input)
        {
            Regex regex = new Regex(@"^(?<c1>\w+) to (?<c2>\w+) = (?<distance>\d+)$");
            var result = new Dictionary<string, Dictionary<string, int>>();

            void Add(string city1, string city2, int distance)
            {
                if (!result.TryGetValue(city1, out var dict))
                {
                    dict = new Dictionary<string, int>();
                    result[city1] = dict;
                }

                dict[city2] = distance;
            }

            string? line;
            while ((line = input.ReadLine()) != null)
            {
                Match match = regex.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Error parsing line: " + line);
                }

                var c1 = match.Groups["c1"].Value;
                var c2 = match.Groups["c2"].Value;
                var distance = int.Parse(match.Groups["distance"].ValueSpan);

                Add(c1, c2, distance);
                Add(c2, c1, distance);
            }

            return result;
        }
    }
}
