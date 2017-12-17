using System;
using System.Collections.Generic;
using System.IO;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day13
    {
        private ITestOutputHelper _output;
        private string _input;

        private const string _sample = @"0: 3
1: 2
4: 4
6: 4";

        public Day13(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(13);
        }

        [Fact]
        public void Sample()
        {
            var scanners = ParseFirewall(_sample);
            var severity = EnterFirewall(scanners, 0);
            Assert.Equal(24, severity);

            var firstSafeDelay = FindFirstSafeDelay(scanners);
            Assert.Equal(10, firstSafeDelay);
        }

        [Fact]
        public void Puzzle()
        {
            var scanners = ParseFirewall(_input);
            var severity = EnterFirewall(scanners, 0);
            _output.WriteLine("part 1 = {0}", severity);

            var firstSafeDelay = FindFirstSafeDelay(scanners);
            _output.WriteLine("part 2= {0}", firstSafeDelay);
        }

        private int? EnterFirewall(List<(int, int)> scanners, int delay)
        {
            int severity = 0;
            bool caught = false;
            foreach (var (depth, range) in scanners)
            {
                if ((depth + delay) % (2 * range - 2) == 0)
                {
                    severity = severity + depth * range;
                    caught = true;
                }
            }

            return caught ? (int?)severity : null;
        }

        private List<(int depth, int range)> ParseFirewall(string input)
        {
            List<(int depth, int range)> scanners = new List<(int depth, int range)>();
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int index = line.IndexOf(": ");
                    string str = line.Substring(0, index);
                    int depth = int.Parse(str);
                    str = line.Substring(index + 2);
                    int range = int.Parse(str);
                    scanners.Add((depth, range));
                }
            }
            return scanners;
        }

        private int FindFirstSafeDelay(List<(int,int)> scanners)
        {
            for (int i = 0; i >= 0; i++)
            {
                if (EnterFirewall(scanners, i) == null)
                {
                    return i;
                }
            }

            throw new ArgumentException("Solution couldn't be found for provided input");
        }
    }
}
