using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day03
    {
        private ITestOutputHelper _output;

        public Day03(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData("1", 0)]
        [InlineData("12", 3)]
        [InlineData("23", 2)]
        [InlineData("1024", 31)]
        public void Part1Samples(string input, int expected)
        {
            int distance = ManhattanDistance(input);
            Assert.Equal(expected, distance);
        }

        [Fact]
        public void Part1()
        {
            string input = GetPuzzleInput.DayText(3);
            int distance = ManhattanDistance(input);
            _output.WriteLine("distance = {0}", distance);
        }

        [Fact]
        public void Part2()
        {
            string input = GetPuzzleInput.DayText(3);
            int minValue = int.Parse(input);
            Dictionary<Location, int> memory = new Dictionary<Location, int>();

            var enumerator = new LocationEnumerator();
            enumerator.MoveNext();
            memory[enumerator.Current] = 1;
            int lastValue = 1;

            while (lastValue < minValue)
            {
                enumerator.MoveNext();
                int sum = 0;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        Location l = new Location
                        {
                            X = enumerator.Current.X + x,
                            Y = enumerator.Current.Y + y
                        };
                        int locationValue;
                        if (memory.TryGetValue(l, out locationValue))
                        {
                            sum += locationValue;
                        }
                    }
                }
                memory[enumerator.Current] = sum;
                lastValue = sum;
            }

            _output.WriteLine("value = {0}", lastValue);
        }

        private int ManhattanDistance(string input)
        {
            int target = int.Parse(input);
            Locations locations = new Locations();

            var l = locations.Skip(target - 1).First();

            int x = l.X >= 0 ? l.X : -l.X;
            int y = l.Y >= 0 ? l.Y : -l.Y;

            return x + y;
        }

        private struct Location
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private class LocationEnumerator : IEnumerator<Location>
        {
            private Location? _location;

            public Location Current => _location.Value;

            object IEnumerator.Current => _location.Value;

            int direction;
            int x;
            int y;
            int segmentLength;
            int remaining;
            int memoryLocation;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_location == null)
                {
                    direction = 3;
                    x = 0;
                    y = 0;
                    segmentLength = 0;
                    remaining = 0;
                    memoryLocation = 1;

                    _location = new Location()
                    {
                        X = x,
                        Y = y
                    };
                }
                else
                {
                    if (remaining <= 0)
                    {
                        direction = (direction + 1) % 4;
                        if (direction % 2 == 0)
                        {
                            segmentLength++;
                        }
                        remaining = segmentLength;
                    }

                    memoryLocation++;
                    remaining--;
                    if (direction == 0) x++;
                    else if (direction == 1) y++;
                    else if (direction == 2) x--;
                    else if (direction == 3) y--;
                    else throw new Exception();

                    _location = new Location()
                    {
                        X = x,
                        Y = y
                    };
                }
                return true;
            }

            public void Reset()
            {
                _location = null;
            }
        }

        private class Locations : IEnumerable<Location>
        {
            public IEnumerator<Location> GetEnumerator()
            {
                return new LocationEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new LocationEnumerator();
            }
        }
    }
}
