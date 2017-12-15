using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day15
    {
        private ITestOutputHelper _output;
        private string _input;

        private static Regex _regex = new Regex("^Generator (?<generator>(A|B)) starts with (?<value>\\d+)\r?$", RegexOptions.Multiline);

        private const int GeneratorAMultiplier = 16807;
        private const int GeneratorBMultiplier = 48271;

        public Day15(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(15);
        }

        [Fact]
        public void Sample()
        {
            var matches = CountMatches(Generator(65, GeneratorAMultiplier, 1),
                Generator(8921, GeneratorBMultiplier, 1),
                40_000_000);
            Assert.Equal(588, matches);

            matches = CountMatches(Generator(65, GeneratorAMultiplier, 4),
                Generator(8921, GeneratorBMultiplier, 8),
                5_000_000);
            Assert.Equal(309, matches);
        }

        [Fact]
        public void Puzzle()
        {
            int parseValue(Match match, string generator)
            {
                if (match.Groups["generator"].Value != generator) throw new Exception();
                return int.Parse(match.Groups["value"].Value);
            }

            var regexResults = _regex.Matches(_input);
            if (regexResults.Count != 2) throw new Exception();
            var a = parseValue(regexResults[0], "A");
            var b = parseValue(regexResults[1], "B");

            var matches = CountMatches(Generator(a, GeneratorAMultiplier, 1),
                Generator(b, GeneratorBMultiplier, 1),
                40_000_000);
            _output.WriteLine("part 1 = {0}", matches);

            matches = CountMatches(Generator(a, GeneratorAMultiplier, 4),
                Generator(b, GeneratorBMultiplier, 8),
                5_000_000);
            _output.WriteLine("part 2 = {0}", matches);
        }

        private int CountMatches(IEnumerable<long> a, IEnumerable<long> b, int pairs)
        {
            return a.Zip(b, (av, bv) => (av ^ bv) & 0xFFFF)
                .Take(pairs)
                .Count(v => v == 0);
        }

        private IEnumerable<long> Generator(int initialValue, int multiplier, int multiple)
        {
            long value = initialValue;
            for (; ; )
            {
                value = (value * multiplier) % 2147483647;
                if ((value % multiple) == 0)
                {
                    yield return value;
                }
            }
        }
    }
}
