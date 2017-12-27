using System.Collections.Generic;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day14
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day14(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(14);
        }

        [Fact]
        public void Sample()
        {
            string input = "flqrgnkx";
            var used = GetUsed(input);
            Assert.Equal(8108, used.Count);

            var groups = Group(used);
            Assert.Equal(1242, groups.Count);
        }

        [Fact]
        public void Puzzle()
        {
            var used = GetUsed(_input);
            _output.WriteLine("used = {0}", used.Count);
            var groups = Group(used);
            _output.WriteLine("groups = {0}", groups.Count);
        }

        private List<HashSet<Position>> Group(HashSet<Position> used)
        {
            HashSet<Position> ungrouped = new HashSet<Position>(used.Count);
            foreach (var p in used)
            {
                ungrouped.Add(p);
            }

            List<HashSet<Position>> groups = new List<HashSet<Position>>();
            Stack<Position> toCheck = new Stack<Position>();

            while (ungrouped.Count > 0)
            {
                HashSet<Position> group = new HashSet<Position>();
                Position pos = ungrouped.First();
                toCheck.Push(pos);

                while (toCheck.Count > 0)
                {
                    var check = toCheck.Pop();
                    if (ungrouped.Contains(check))
                    {
                        ungrouped.Remove(check);
                        group.Add(check);

                        toCheck.Push(new Position { X = check.X + 1, Y = check.Y });
                        toCheck.Push(new Position { X = check.X - 1, Y = check.Y });
                        toCheck.Push(new Position { X = check.X, Y = check.Y + 1 });
                        toCheck.Push(new Position { X = check.X, Y = check.Y - 1 });
                    }
                }

                groups.Add(group);
            }

            return groups;
        }

        private HashSet<Position> GetUsed(string input)
        {
            HashSet<Position> used = new HashSet<Position>();

            for (int row = 0; row <= 127; row++)
            {
                string rowInput = input + "-" + row;
                byte[] bytes = Day10.KnotHashBytes(rowInput);
                int column = 0;
                for (var i = 0; i < bytes.Length; i++)
                {
                    byte copy = bytes[i];
                    for (byte bit = 0x80; bit > 0; bit = (byte)(bit >> 1), column++)
                    {
                        if ((copy & bit) != 0)
                        {
                            used.Add(new Position { X = column, Y = row });
                        }
                    }
                }
            }

            return used;
        }

        [System.Diagnostics.DebuggerDisplay("{X},{Y}")]
        private struct Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
