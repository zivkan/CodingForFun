using System;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day11
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day11(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(11);
        }

        [Theory]
        [InlineData("ne,ne,ne", 3)]
        [InlineData("ne,ne,sw,sw", 0)]
        [InlineData("ne,ne,s,s", 2)]
        [InlineData("se,sw,se,sw,sw", 3)]
        public void Sample(string input, int expected)
        {
            var (last,max) = GetDistances(input);
            Assert.Equal(expected, last);
        }

        [Fact]
        public void Puzzle()
        {
            var (last, max) = GetDistances(_input);
            _output.WriteLine("final distance = {0}", last);
            _output.WriteLine("furthest distance = {0}", max);
        }

        private int GetDistance(int x, int y, int z)
        {
            return (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;
        }

        private (int last, int max) GetDistances(string input)
        {
            int x = 0, y = 0, z = 0;
            int distance = 0;
            int maxDistance = 0;

            var directions = input.Split(',');

            foreach(var direction in directions)
            {
                switch (direction)
                {
                    case "n":
                        x += 1;
                        y -= 1;
                        break;

                    case "ne":
                        x += 1;
                        z -= 1;
                        break;

                    case "se":
                        y += 1;
                        z -= 1;
                        break;

                    case "s":
                        x -= 1;
                        y += 1;
                        break;

                    case "sw":
                        x -= 1;
                        z += 1;
                        break;

                    case "nw":
                        y -= 1;
                        z += 1;
                        break;

                    default:
                        throw new ArgumentException(direction);
                }

                distance = GetDistance(x, y, z);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }

            return (distance, maxDistance);
        }
    }
}
