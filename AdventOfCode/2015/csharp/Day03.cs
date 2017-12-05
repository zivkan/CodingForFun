using System;
using System.Collections.Generic;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day03
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day03(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(3);
        }

        [Theory]
        [InlineData(">", 2)]
        [InlineData("^>v<", 4)]
        [InlineData("^v^v^v^v^v", 2)]
        public void Part1Sample(string input, int expected)
        {
            int houses = DeliverPresents(input, 1);
            Assert.Equal(expected, houses);
        }

        [Fact]
        public void Part1()
        {
            int houses = DeliverPresents(_input, 1);
            _output.WriteLine("houses = {0}", houses);
        }

        [Theory]
        [InlineData("^v", 3)]
        [InlineData("^>v<", 3)]
        [InlineData("^v^v^v^v^v", 11)]
        public void Part2Sample(string input, int expected)
        {
            int houses = DeliverPresents(input, 2);
            Assert.Equal(expected, houses);
        }

        [Fact]
        public void Part2()
        {
            int houses = DeliverPresents(_input, 2);
            _output.WriteLine("Houses = {0}", houses);
        }

        private int DeliverPresents(string input, int santas)
        {
            HashSet<Location> houses = new HashSet<Location>();
            Location[] locations = new Location[santas];
            foreach (var i in Enumerable.Range(0, santas))
            {
                locations[i] = new Location {X = 0, Y = 0};
            }
            int turn = 0;

            houses.Add(locations[0]);

            foreach (var direction in input)
            {
                Location location = locations[turn];

                if (direction == '>')
                {
                    location = new Location {X = location.X + 1, Y = location.Y};
                }
                else if (direction == 'v')
                {
                    location = new Location {X = location.X, Y = location.Y - 1};
                }
                else if (direction == '<')
                {
                    location = new Location {X = location.X - 1, Y = location.Y};
                }
                else if (direction == '^')
                {
                    location = new Location {X = location.X, Y = location.Y + 1};
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (!houses.Contains(location))
                {
                    houses.Add(location);
                }

                locations[turn] = location;
                turn = (turn + 1) % santas;
            }

            return houses.Count;
        }

        private struct Location
        {
            public int X;
            public int Y;
        }
    }
}
