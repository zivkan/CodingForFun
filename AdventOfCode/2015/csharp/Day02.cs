using System;
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

        [Theory]
        [InlineData("2x3x4", 58)]
        [InlineData("1x1x10", 43)]
        public void Part1Sample(string input, int expected)
        {
            int area = CalculateRequiredArea(input);
            Assert.Equal(expected, area);
        }

        [Fact]
        public void Part1()
        {
            int area = CalculateRequiredArea(_input);
            _output.WriteLine("area = {0}", area);
        }

        [Theory]
        [InlineData("2x3x4", 34)]
        [InlineData("1x1x10", 14)]
        public void Part2Sample(string input, int expected)
        {
            int area = CalculateRibbon(input);
            Assert.Equal(expected, area);
        }

        [Fact]
        public void Part2()
        {
            int area = CalculateRibbon(_input);
            _output.WriteLine("area = {0}", area);
        }

        private int CalculateRequiredArea(string input)
        {
            int total = 0;
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split('x');
                    var dimensions = split.Select(int.Parse).ToList();
                    int smallestSide = dimensions.OrderBy(i => i).Take(2).Aggregate(1, (acc, i) => acc * i);
                    total += 2 * dimensions[0] * dimensions[1] +
                        2 * dimensions[0] * dimensions[2] +
                        2 * dimensions[1] * dimensions[2] +
                        smallestSide;
                }
            }

            return total;
        }

        private int CalculateRibbon(string input)
        {
            int total = 0;
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split('x');
                    var dimensions = split.Select(int.Parse).ToList();
                    int wrapping = dimensions.OrderBy(i => i).Take(2).Aggregate(0, (acc, i) => acc + 2 * i);
                    int bow = dimensions[0] * dimensions[1] * dimensions[2];
                    total += wrapping + bow;
                }
            }

            return total;
        }
    }
}
