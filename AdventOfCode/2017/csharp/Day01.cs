using System;
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
        [InlineData("1122", 3)]
        [InlineData("1111", 4)]
        [InlineData("1234", 0)]
        [InlineData("91212129", 9)]
        public void Part1Sample(string input, int expected)
        {
            int result = SolveCaptcha(input, Next);
            _output.WriteLine("{0}", result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1()
        {
            int result = SolveCaptcha(_input, Next);
            _output.WriteLine("{0}", result);
        }

        [Theory]
        [InlineData("1212", 6)]
        [InlineData("1221", 0)]
        [InlineData("123425", 4)]
        [InlineData("123123", 12)]
        [InlineData("12131415", 4)]
        public void Part2Sample(string input, int expected)
        {
            int result = SolveCaptcha(input, Opposite);
            _output.WriteLine("{0}", result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2()
        {
            int result = SolveCaptcha(_input, Opposite);
            _output.WriteLine("{0}", result);
        }

        private int SolveCaptcha(string input, Func<int, int, int> compareIndex)
        {
            int sum = 0;
            int length = input.Length;
            for (int i = 0; i < length; i++)
            {
                int compare = compareIndex(i, length);
                if (input[i] == input[compare])
                {
                    sum += input[i] - '0';
                }
            }

            return sum;
        }

        private int Next(int index, int length)
        {
            return (index + 1) % length;
        }

        private int Opposite(int index, int length)
        {
            return (index + (length / 2)) % length;
        }
    }
}
