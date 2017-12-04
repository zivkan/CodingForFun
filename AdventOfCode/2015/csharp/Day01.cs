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
        [InlineData("(())", 0)]
        [InlineData("()()", 0)]
        [InlineData("(((", 3)]
        [InlineData("(()(()(", 3)]
        [InlineData("))(((((", 3)]
        [InlineData("())", -1)]
        [InlineData("))(", -1)]
        [InlineData(")))", -3)]
        [InlineData(")())())", -3)]
        public void Part1Sample(string input, int expected)
        {
            int? firstBasementIndex;
            int floor = GetFloorNumber(input, out firstBasementIndex);
            Assert.Equal(expected, floor);
        }

        [Fact]
        public void Part1()
        {
            int? firstBasementIndex;
            int floor = GetFloorNumber(_input, out firstBasementIndex);
            _output.WriteLine("Floor = {0}", floor);
        }

        [Theory]
        [InlineData(")", 1)]
        [InlineData("()())", 5)]
        public void Part2Sample(string input, int expected)
        {
            int? firstBasementIndex;
            GetFloorNumber(input, out firstBasementIndex);
            Assert.Equal(expected, firstBasementIndex);
        }

        [Fact]
        public void Part2()
        {
            int? firstBasementIndex;
            GetFloorNumber(_input, out firstBasementIndex);
            _output.WriteLine("First basement index = {0}", firstBasementIndex);
        }

        private int GetFloorNumber(string input, out int? firstBasementIndex)
        {
            int floor = 0;
            firstBasementIndex = null;
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c == '(')
                {
                    floor++;
                }
                else
                {
                    floor--;
                }
                if (firstBasementIndex == null && floor == -1)
                {
                    firstBasementIndex = i + 1;
                }
            }
            return floor;
        }
    }
}
