using System;
using System.Collections.Generic;
using System.IO;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day05
    {
        private ITestOutputHelper _output;
        private string _input;

        private const string SampleInput = "0\n3\n0\n1\n-3";

        public Day05(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(5);
        }

        [Theory]
        [InlineData(SampleInput, 5, new[] {2, 5, 0, 1, -2})]
        public void Part1Sample(string input, int expectedSteps, int[] expectedOffsets)
        {
            var (steps, offsets) = CountSteps(input, Part1Increment);
            Assert.Equal(expectedSteps, steps);
            Assert.Equal(expectedOffsets, offsets);
        }

        [Fact]
        public void Part1()
        {
            var (steps, offsets) = CountSteps(_input, Part1Increment);
            _output.WriteLine("Result = {0}", steps);
        }

        [Theory]
        [InlineData(SampleInput, 10, new[] { 2, 3, 2, 3, -1 })]
        public void Part2Sample(string input, int expectedSteps, int[] expectedOffsets)
        {
            var (steps, offsets) = CountSteps(input, Part2Increment);
            Assert.Equal(expectedSteps, steps);
            Assert.Equal(expectedOffsets, offsets);
        }

        [Fact]
        public void Part2()
        {
            var (steps, offsets) = CountSteps(_input, Part2Increment);
            _output.WriteLine("Result = {0}", steps);
        }

        private (int steps, List<int> offsets) CountSteps(string input, Func<int, int> newValue)
        {
            List<int> offsets = new List<int>();
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int value = int.Parse(line);
                    offsets.Add(value);
                }
            }

            int step = 0;
            int position = 0;

            while (position >= 0 && position < offsets.Count)
            {
                step++;
                int offset = offsets[position];
                offsets[position] = newValue(offset);
                position += offset;
            }

            return (step, offsets);
        }

        private int Part1Increment(int value)
        {
            return value + 1;
        }

        private int Part2Increment(int value)
        {
            return value >= 3
                ? value - 1
                : value + 1;
        }
    }
}
