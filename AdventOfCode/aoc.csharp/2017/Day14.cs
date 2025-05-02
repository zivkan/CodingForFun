using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2017
{
    public class Day14 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadLine();
            var used = GetUsed(text);
            var part1 = used.Count;
            var groups = Group(used);
            var part2 = groups.Count;
            return (part1.ToString(), part2.ToString());
        }

        public static List<HashSet<Position>> Group(HashSet<Position> used)
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

        public static HashSet<Position> GetUsed(string input)
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
        public struct Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
