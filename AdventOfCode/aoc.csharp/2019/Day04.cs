using System;
using System.IO;

namespace aoc.csharp._2019
{
    public class Day04 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadLine();
            var seperator = text.IndexOf('-');
            var current = text.AsSpan(0, seperator).ToArray();
            var max = text.AsSpan(seperator + 1);

            var part1 = 0;
            var part2 = 0;

            do
            {
                if (Part1Password(current))
                {
                    part1++;

                    if (Part2Password(current))
                    {
                        part2++;
                    }
                }

                Increment(current);
            } while (!Equal(current, max));

            return (part1.ToString(), part2.ToString());
        }

        private static bool Part2Password(char[] current)
        {
            int count = 0;
            char c = '\0';

            for (int pos = 0; pos < current.Length; pos++)
            {
                var currentChar = current[pos];
                if (currentChar == c)
                {
                    count++;
                }
                else
                {
                    if (count == 2)
                    {
                        return true;
                    }
                    else
                    {
                        count = 1;
                        c = currentChar;
                    }
                }
            }

            return count == 2;
        }

        private static bool Part1Password(char[] current)
        {
            var containsDouble = false;

            for (int i = 1; i < current.Length; i++)
            {
                var c1 = current[i - 1];
                var c2 = current[i];

                if (c1 == c2)
                {
                    containsDouble = true;
                }
                else if (c1 > c2)
                {
                    return false;
                }
            }

            return containsDouble;
        }

        private static void Increment(char[] current)
        {
            for (int i = current.Length -1; i >= 0; i--)
            {
                if (current[i] < '9')
                {
                    current[i]++;
                    break;
                }
                else
                {
                    current[i] = '0';
                }
            }
        }

        private static bool Equal(char[] current, ReadOnlySpan<char> max)
        {
            if (current.Length != max.Length)
            {
                throw new ArgumentException("Values are of different lengths");
            }

            for (int i = 0; i < current.Length; i++)
            {
                if (current[i] != max[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
