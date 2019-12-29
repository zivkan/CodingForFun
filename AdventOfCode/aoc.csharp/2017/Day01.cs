using System;
using System.IO;

namespace aoc.csharp._2017
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
            var part1 = SolveCaptcha(text, Next);
            var part2 = SolveCaptcha(text, Opposite);
            return (part1.ToString(), part2.ToString());
        }

        public static int SolveCaptcha(string input, Func<int, int, int> compareIndex)
        {
            int sum = 0;
            int length = input.Length;
            for (int i = 0; i < length; i++)
            {
                int compare = compareIndex(i, length);
                if (input[i] == input[compare])
                {
                    sum += input[i] - '0';
                }
            }

            return sum;
        }

        public static int Next(int index, int length)
        {
            return (index + 1) % length;
        }

        public static int Opposite(int index, int length)
        {
            return (index + (length / 2)) % length;
        }
    }
}
