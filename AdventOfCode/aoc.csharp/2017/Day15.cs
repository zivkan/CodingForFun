using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2017
{
    public class Day15 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            static int parseValue(Match match, string generator)
            {
                if (match.Groups["generator"].Value != generator) throw new Exception();
                return int.Parse(match.Groups["value"].Value);
            }

            var text = input.ReadToEnd();
            var regexResults = _regex.Matches(text);
            if (regexResults.Count != 2) throw new Exception();
            var a = parseValue(regexResults[0], "A");
            var b = parseValue(regexResults[1], "B");

            var part1 = CountMatches(Generator(a, GeneratorAMultiplier, 1),
                Generator(b, GeneratorBMultiplier, 1),
                40_000_000);
            var part2 = CountMatches(Generator(a, GeneratorAMultiplier, 4),
                Generator(b, GeneratorBMultiplier, 8),
                5_000_000);

            return (part1.ToString(), part2.ToString());
        }

        private static readonly Regex _regex = new Regex("^Generator (?<generator>(A|B)) starts with (?<value>\\d+)\r?$", RegexOptions.Multiline);

        public const int GeneratorAMultiplier = 16807;
        public const int GeneratorBMultiplier = 48271;

        public static int CountMatches(IEnumerable<long> a, IEnumerable<long> b, int pairs)
        {
            return a.Zip(b, (av, bv) => (av ^ bv) & 0xFFFF)
                .Take(pairs)
                .Count(v => v == 0);
        }

        public static IEnumerable<long> Generator(int initialValue, int multiplier, int multiple)
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
