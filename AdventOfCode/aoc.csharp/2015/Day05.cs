using System;
using System.IO;

namespace aoc.csharp._2015
{
    public class Day05 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            string[]? lines = Input.GetLines(input);

            int part1 = 0;
            int part2 = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (Part1(line))
                {
                    part1++;
                }

                if (Part2(line))
                {
                    part2++;
                }
            }

            return (part1.ToString(), part2.ToString());
        }

        public static bool Part1(string input)
        {
            int vowels = 0;
            bool hasDouble = false;
            bool hasForbiddenString = false;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                {
                    vowels++;
                }

                if (i > 0)
                {
                    char prev = input[i - 1];
                    if (prev == c)
                    {
                        hasDouble = true;
                    }

                    if ((prev == 'a' && c == 'b') ||
                        (prev == 'c' && c == 'd') ||
                        (prev == 'p' && c == 'q') ||
                        (prev == 'x' && c == 'y'))
                    {
                        hasForbiddenString = true;
                    }
                }
            }

            var isNice = vowels >= 3 && hasDouble && !hasForbiddenString;
            return isNice;
        }

        public static bool Part2(string input)
        {
            bool hasPair = false;
            for (int i = 1; !hasPair && i < input.Length; i++)
            {
                var candidate = input.AsSpan(i - 1, 2);

                for (var j = i + 2; !hasPair && j < input.Length; j++)
                {
                    var check = input.AsSpan(j - 1, 2);
                    if (check.SequenceEqual(candidate))
                    {
                        hasPair = true;
                    }
                }
            }

            if (!hasPair)
            {
                return false;
            }

            var hasDouble = false;
            for (int i = 2; !hasDouble && i < input.Length; i++)
            {
                if (input[i - 2] == input[i])
                {
                    hasDouble = true;
                }
            }

            return hasDouble;
        }
    }
}
