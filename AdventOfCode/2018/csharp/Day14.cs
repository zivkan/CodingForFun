using System;
using System.Collections.Generic;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day14
    {
        private readonly ITestOutputHelper _output;
        private readonly static string _input = GetInput.Day(14);

        public Day14(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(9, "5158916779")]
        [InlineData(5, "0124515891")]
        [InlineData(18, "9251071085")]
        [InlineData(2018, "5941429882")]
        public void Part1Sample(int iterations, string expected)
        {
            var score = GetScore(iterations);
            Assert.Equal(expected, score);
        }

        [Fact]
        public void Part1()
        {
            var iterations = int.Parse(_input);
            var score = GetScore(iterations);
            Assert.Equal("3147574107", score);
        }

        [Theory]
        [InlineData("51589", 9)]
        [InlineData("01245", 5)]
        [InlineData("92510", 18)]
        [InlineData("59414", 2018)]
        public void Part2Sample(string input, int expected)
        {
            var iterations = GetIterations(input);
            Assert.Equal(expected, iterations);
        }

        [Fact]
        public void Part2()
        {
            var iterations = GetIterations(_input);
            Assert.Equal(20280190, iterations);
        }

        private string GetScore(int iterations)
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

        private int GetIterations(string input)
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

        private void NextRecipe(List<byte> recipes, int[] elves)
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
