using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day02
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day02(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(2);
        }

        [Fact]
        public void Part1Sample()
        {
            string input =
                "abcdef\n" +
                "bababc\n" +
                "abbcde\n" +
                "abcccd\n" +
                "aabcdd\n" +
                "abcdee\n" +
                "ababab";
            int result = CalculateChecksum(input);
            Assert.Equal(12, result);
        }

        [Fact]
        public void Part1()
        {
            int result = CalculateChecksum(_input);
            _output.WriteLine("{0}", result);
        }

        [Fact]
        public void Part2Sample()
        {
            string input =
                "abcde\n" +
                "fghij\n" +
                "klmno\n" +
                "pqrst\n" +
                "fguij\n" +
                "axcye\n" +
                "wvxyz";
            string result = FindCorrectBoxCommonChars(input);
            Assert.Equal("fgij", result);
        }

        [Fact]
        public void Part2()
        {
            string result = FindCorrectBoxCommonChars(_input);
            _output.WriteLine(result);
        }

        private int CalculateChecksum(string input)
        {
            var result = ReadLines(input)
                .Select(ConvertToCharCounts)
                .Select(i => new { Two = i.Count(j => j.Value == 2), Three = i.Count(j => j.Value == 3) })
                .Aggregate(new { Two = 0, Three = 0 }, (a, o) => new { Two = a.Two + (o.Two > 0 ? 1 : 0), Three = a.Three + (o.Three > 0 ? 1 : 0) });
            return result.Two * result.Three;
        }

        private IEnumerable<string> ReadLines(string input)
        {
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private Dictionary<char, int> ConvertToCharCounts(string line)
        {
            return line.GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        private string FindCorrectBoxCommonChars(string input)
        {
            var lines = ReadLines(input).ToList();

            for (int i = 0; i < lines.Count - 1; i++)
            {
                for (int j = i+1; j < lines.Count; j++)
                {
                    var differences = CountDifferences(lines[i], lines[j]);
                    if (differences == 1)
                    {
                        int index = FindIndexOfDifference(lines[i], lines[j]);
                        return lines[i].Remove(index, 1);
                    }
                }
            }

            throw new Exception();
        }

        private int CountDifferences(string v1, string v2)
        {
            if (v1.Length != v2.Length) throw new InvalidOperationException();

            int differences = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                if (v1[i] != v2[i])
                {
                    differences++;
                }
            }

            return differences;
        }

        private int FindIndexOfDifference(string v1, string v2)
        {
            for (int i = 0; i < v1.Length; i++)
            {
                if (v1[i] != v2[i]) return i;
            }

            throw new ArgumentException();
        }
    }
}
