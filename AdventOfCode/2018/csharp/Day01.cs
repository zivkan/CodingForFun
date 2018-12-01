using System.Collections.Generic;
using System.IO;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day01
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day01(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(1);
        }

        [Theory]
        [InlineData("+1\n-2\n+3\n+1", 3)]
        [InlineData("+1\n+1\n+1", 3)]
        [InlineData("+1\n+1\n-2", 0)]
        [InlineData("-1\n-2\n-3", -6)]
        public void Part1Sample(string input, int expected)
        {
            int result = AccumulateChanges(0, input).Last();
            _output.WriteLine("{0}", result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1()
        {
            int result = AccumulateChanges(0, _input).Last();
            _output.WriteLine("{0}", result);
        }

        [Theory]
        [InlineData("+1\n-2\n+3\n+1", 2)]
        [InlineData("+1\n-1", 0)]
        [InlineData("+3\n+3\n+4\n-2\n-4", 10)]
        [InlineData("-6\n+3\n+8\n+5\n-6", 5)]
        [InlineData("+7\n+7\n-2\n-7\n-4", 14)]
        public void Part2Sample(string input, int? expected)
        {
            var result = FirstReoccurance(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2()
        {
            var result = FirstReoccurance(_input);
            _output.WriteLine("{0}", result);
        }

        private IEnumerable<int> AccumulateChanges(int initial, string input)
        {
            var freq = initial;
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var change = int.Parse(line);
                    freq += change;
                    yield return freq;
                }
            }
        }

        private int? FirstReoccurance(string input)
        {
            var seen = new HashSet<int>();
            int initial = 0;
            seen.Add(initial);
            // no infinite loop
            for (int i = 0; i < 1000; i++)
            {
                foreach (var freq in AccumulateChanges(initial, input))
                {
                    if (!seen.Add(freq))
                    {
                        return freq;
                    }
                    initial = freq;
                }
            }

            return null;
        }
    }
}
