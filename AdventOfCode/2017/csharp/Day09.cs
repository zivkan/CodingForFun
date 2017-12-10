using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day09
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day09(ITestOutputHelper output)
        {
            _output = output;
            _input = GetPuzzleInput.DayText(9);
        }

        [Theory]
        [InlineData("{}", 1)]
        [InlineData("{{{}}}", 6)]
        [InlineData("{{},{}}", 5)]
        [InlineData("{{{},{},{{}}}}", 16)]
        [InlineData("{<a>,<a>,<a>,<a>}", 1)]
        [InlineData("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)]
        [InlineData("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9)]
        [InlineData("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
        public void Part1Sample(string input, int expected)
        {
            var (score,chars) = GetScore(input);
            Assert.Equal(expected, score);
        }

        [Fact]
        public void Part1()
        {
            var (score, chars) = GetScore(_input);
            _output.WriteLine("Score = {0}", score);
        }

        [Theory]
        [InlineData("<>", 0)]
        [InlineData("<random characters>", 17)]
        [InlineData("<<<<>", 3)]
        [InlineData("<{!>}>", 2)]
        [InlineData("<!!>", 0)]
        [InlineData("<!!!>>", 0)]
        [InlineData("<{o\"i!a,<{i<a>", 10)]
        public void Part2Sample(string input, int expected)
        {
            var (score, chars) = GetScore(input);
            Assert.Equal(expected, chars);
        }

        [Fact]
        public void Part2()
        {
            var (score, chars) = GetScore(_input);
            _output.WriteLine("Chars = {0}", chars);
        }

        private (int score, int chars) GetScore(string input)
        {
            List<int> groups = new List<int>();
            int depth = 0;
            int chars = 0;
            bool inGarbage = false;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '{' && !inGarbage)
                {
                    depth++;
                    groups.Add(depth);
                }
                else if (c == '}' && !inGarbage)
                {
                    depth--;
                }
                else if (c == '<' && !inGarbage)
                {
                    inGarbage = true;
                }
                else if (c == '>')
                {
                    inGarbage = false;
                }
                else if (c == '!')
                {
                    i++;
                }
                else if (inGarbage)
                {
                    chars++;
                }
            }

            return (groups.Sum(), chars);
        }
    }
}
