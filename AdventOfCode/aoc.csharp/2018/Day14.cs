using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2018
{
    public class Day14 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadLine();
            var iterations = int.Parse(text);
            var part1 = GetScore(iterations);
            var part2 = GetIterations(text);

            return (part1, part2.ToString());
        }

        public static string GetScore(int iterations)
        {
            var recipes = new List<byte>() { 3, 7 };
            var elves = new[] { 0, 1 };

            var target = iterations + 10;

            while (recipes.Count < target)
            {
                NextRecipe(recipes, elves);
            }

            var str = new char[10];
            for (int i = 0; i < 10; i++)
            {
                str[i] = (char)(recipes[i + iterations] + '0');
            }

            return new string(str);
        }

        public static int GetIterations(string input)
        {
            var recipes = new List<byte>() { 3, 7 };
            var elves = new[] { 0, 1 };
            var expected = new byte[input.Length];
            for (int i = 0; i < input.Length; i++) expected[i] = (byte)(input[i] - '0');

            for (var index = 0; index < 100_000_000; index++)
            {
                while (recipes.Count < index + input.Length) NextRecipe(recipes, elves);

                bool found = true;
                for (int i = 0; i < expected.Length; i++)
                {
                    if (expected[i] != recipes[index + i])
                    {
                        found = false;
                        break;
                    }
                }

                if (found) return index;
            }

            throw new Exception("infinite loop");
        }

        private static void NextRecipe(List<byte> recipes, int[] elves)
        {
            var newRecipe = recipes[elves[0]] + recipes[elves[1]];
            if (newRecipe >= 10)
            {
                recipes.Add((byte)(newRecipe / 10));
                recipes.Add((byte)(newRecipe % 10));
            }
            else
            {
                recipes.Add((byte)newRecipe);
            }

            elves[0] = (elves[0] + recipes[elves[0]] + 1) % recipes.Count;
            elves[1] = (elves[1] + recipes[elves[1]] + 1) % recipes.Count;
        }
    }
}
