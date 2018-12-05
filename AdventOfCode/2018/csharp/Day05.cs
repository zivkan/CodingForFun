using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day05
    {
        private ITestOutputHelper _output;
        private string _input;
        private static readonly string _sampleInput = "dabAcCaCBAcCcaDA";

        public Day05(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(5);
        }

        [Fact]
        public void Part1Sample()
        {
            var result = React(_sampleInput);
            Assert.Equal(10, result.Length);
        }

        [Fact]
        public void Part1()
        {
            var result = React(_input);
            _output.WriteLine("{0}", result.Length);
        }

        [Fact]
        public void Part2Sample()
        {
            var result = BestReaction(_sampleInput);
            Assert.Equal(4, result.Length);
        }

        [Fact]
        public void Part2()
        {
            var result = BestReaction(_input);
            _output.WriteLine("{0}", result.Length);
        }

        private string React(string input)
        {
            var reactions = new List<string>();
            var polymers = new char[2];
            for (char polymer = 'a'; polymer <= 'z'; polymer++)
            {
                polymers[0] = polymer;
                polymers[1] = char.ToUpper(polymer);
                reactions.Add(new string(polymers));
                polymers[0] = char.ToUpper(polymer);
                polymers[1] = polymer;
                reactions.Add(new string(polymers));
            }

            string result = input;
            int before;
            do
            {
                before = result.Length;
                foreach (var reaction in reactions)
                {
                    result = result.Replace(reaction, string.Empty);
                }
            } while (before != result.Length);

            return result;
        }

        private string BestReaction(string input)
        {
            string best = input;
            char[] chars = new char[1];

            for (char c = 'a'; c <= 'z'; c++)
            {
                chars[0] = c;
                string test = input.Replace(new string(chars), string.Empty);
                chars[0] = char.ToUpperInvariant(c);
                test = test.Replace(new string(chars), string.Empty);

                var result = React(test);
                if (result.Length < best.Length)
                {
                    best = result;
                }
            }

            return best;
        }
    }
}
