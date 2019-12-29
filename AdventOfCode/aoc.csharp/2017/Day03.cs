using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2017
{
    public class Day03 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            int part1 = ManhattanDistance(text);

            int minValue = int.Parse(text);
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
                        if (memory.TryGetValue(l, out int locationValue))
                        {
                            sum += locationValue;
                        }
                    }
                }
                memory[enumerator.Current] = sum;
                lastValue = sum;
            }

            var part2 = lastValue;

            return (part1.ToString(), part2.ToString());
        }

        public static int ManhattanDistance(string input)
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

            public Location Current => _location ?? throw new InvalidOperationException();

            object IEnumerator.Current => _location ?? throw new InvalidOperationException();

            int direction;
            int x;
            int y;
            int segmentLength;
            int remaining;

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
