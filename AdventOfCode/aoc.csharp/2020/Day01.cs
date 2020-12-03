using System;
using System.IO;

namespace aoc.csharp._2020
{
    public class Day01 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var list = Input.ToList<int>(input);

            string? part1 = null;
            string? part2 = null;

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i+1; j < list.Count; j++)
                {
                    if (part1 == null && list[i] + list[j] == 2020)
                    {
                        part1 = (list[i] * list[j]).ToString();

                        if (part2 != null)
                        {
                            return (part1, part2);
                        }
                    }

                    for (int k = j+1; part2 == null && k < list.Count; k++)
                    {
                        if (list[i] + list[j] + list[k] == 2020)
                        {
                            part2 = (list[i] * list[j] * list[k]).ToString();

                            if (part1 != null)
                            {
                                return (part1, part2);
                            }
                        }
                    }
                }
            }

            throw new ArgumentException("Unable to find solution");
        }
    }
}
