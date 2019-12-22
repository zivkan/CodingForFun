using aoc.csharp.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2019
{
    public class Day03 : ISolver
    {
        private static readonly Point2D Left =new Point2D(-1, 0);
        private static readonly Point2D Right = new Point2D(1, 0);
        private static readonly Point2D Up = new Point2D(0, 1);
        private static readonly Point2D Down = new Point2D(0, -1);

        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var linesDefinitions = Input.GetLines(input);
            if (linesDefinitions.Length != 2)
            {
                throw new ArgumentException("Expected 2 lines, got " + linesDefinitions.Length);
            }

            var line1 = GetLinePoints(linesDefinitions[0]);
            var line2 = GetLinePoints(linesDefinitions[1]);

            var intersections = line1.Keys.Intersect(line2.Keys);

            return (ShortestManhattenDistance(intersections).ToString(),
                GetCrossedCircuitLength(intersections, line1, line2).ToString());
        }

        public static uint GetCrossedCircuitLength(IEnumerable<Point2D> intersections, Dictionary<Point2D, uint> line1, Dictionary<Point2D, uint> line2)
        {
            uint min = int.MaxValue;

            foreach (var point in intersections)
            {
                var length1 = line1[point];
                var length2 = line2[point];
                var total = length1 + length2;
                if (total < min)
                {
                    min = total;
                }
            }

            return min;
        }

        public static Dictionary<Point2D, uint> GetLinePoints(string path)
        {
            var points = new Dictionary<Point2D, uint>(Point2DComparer.Instance);
            Point2D position = Point2D.Zero;
            uint length = 0;

            var segments = path.Split(",");
            foreach (var segment in segments)
            {
                var direction = (segment[0]) switch
                {
                    'U' => Up,
                    'D' => Down,
                    'L' => Left,
                    'R' => Right,
                    _ => throw new Exception("Unknown segment " + segment),
                };
                int distance = int.Parse(segment.AsSpan(1));

                for (int i = 0; i < distance; i++)
                {
                    position += direction;
                    length++;
                    points.TryAdd(position, length);
                }
            }

            return points;
        }

        public static uint ShortestManhattenDistance(IEnumerable<Point2D> points)
        {
            return points.Select(p => p.GetManhattenDistance())
                .Min();
        }
    }
}
