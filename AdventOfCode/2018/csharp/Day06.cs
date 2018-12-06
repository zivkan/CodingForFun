using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day06
    {
        private ITestOutputHelper _output;
        private string _input;
        private static readonly string _sampleInput = 
            "1, 1\n" +
            "1, 6\n" +
            "8, 3\n" +
            "3, 4\n" +
            "5, 5\n" +
            "8, 9";

        public Day06(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(6);
        }

        [Fact]
        public void Part1Sample()
        {
            var result = FindLargestArea(_sampleInput);
            Assert.Equal(17, result);
        }

        [Fact]
        public void Part1()
        {
            var result = FindLargestArea(_input);
            _output.WriteLine("{0}", result);
        }

        [Fact]
        public void Part2Sample()
        {
            var result = SizeOfTargetArea(_sampleInput, 32);
            Assert.Equal(16, result);
        }

        [Fact]
        public void Part2()
        {
            var result = SizeOfTargetArea(_input, 10000);
            _output.WriteLine("{0}", result);
        }

        private int FindLargestArea(string input)
        {
            var points = ParsePoints(input);
            var grid = CreateGrid(points);
            FillGridWithClosestId(grid, points);
            var areas = GetAreaPerId(grid);
            return areas.Max(a => a.Value);
        }

        private int SizeOfTargetArea(string input, int threshold)
        {
            var points = ParsePoints(input);
            var grid = CreateGrid(points);
            FillGridWithTotalManhattenDistance(grid, points);

            int count = 0;
            foreach (int distance in grid)
            {
                if (distance < threshold)
                {
                    count++;
                }
            }

            return count;
        }

        private Dictionary<int, int> GetAreaPerId(int[,] grid)
        {
            var areas = new Dictionary<int, int>();

            foreach (var id in grid)
            {
                if (id == 0) continue;

                if (areas.TryGetValue(id, out int area))
                {
                    area++;
                    areas[id] = area;
                }
                else
                {
                    areas[id] = 1;
                }
            }

            int left = grid.GetLowerBound(0);
            int right = grid.GetUpperBound(0);
            int top = grid.GetLowerBound(1);
            int bottom = grid.GetUpperBound(1);

            for (int x = left; x <= right; x++)
            {
                int id = grid[x, top];
                if (areas.ContainsKey(id)) areas.Remove(id);
                id = grid[x, bottom];
                if (areas.ContainsKey(id)) areas.Remove(id);
            }

            for (int y = top; y <= bottom; y++)
            {
                int id = grid[left, y];
                if (areas.ContainsKey(id)) areas.Remove(id);
                id = grid[right, y];
                if (areas.ContainsKey(id)) areas.Remove(id);
            }

            return areas;
        }

        private void FillGridWithClosestId(int[,] grid, List<Point> points)
        {
            var maxY = grid.GetUpperBound(1);
            var maxX = grid.GetUpperBound(0);
            for (int y = grid.GetLowerBound(1); y <= maxY; y++)
            {
                for (int x = grid.GetLowerBound(0); x <= maxX; x++)
                {
                    int minDistance = int.MaxValue;
                    int id = 0;
                    for (int i = 0; i < points.Count; i++)
                    {
                        int dx = x - points[i].X;
                        int dy = y - points[i].Y;
                        int d = (dx >= 0 ? dx : -dx) + (dy >= 0 ? dy : -dy);
                        if (d < minDistance)
                        {
                            minDistance = d;
                            id = points[i].Id;
                        }
                        else if (d == minDistance)
                        {
                            id = 0;
                        }
                    }

                    grid[x, y] = id;
                }
            }
        }

        private void FillGridWithTotalManhattenDistance(int[,] grid, List<Point> points)
        {
            var maxY = grid.GetUpperBound(1);
            var maxX = grid.GetUpperBound(0);
            for (int y = grid.GetLowerBound(1); y <= maxY; y++)
            {
                for (int x = grid.GetLowerBound(0); x <= maxX; x++)
                {
                    int totalDistance = 0;
                    for (int i = 0; i < points.Count; i++)
                    {
                        int dx = x - points[i].X;
                        int dy = y - points[i].Y;
                        int d = (dx >= 0 ? dx : -dx) + (dy >= 0 ? dy : -dy);
                        totalDistance += d;
                    }

                    grid[x, y] = totalDistance;
                }
            }
        }

        private int[,] CreateGrid(List<Point> points)
        {
            int minX = points.Min(p => p.X);
            int minY = points.Min(p => p.Y);
            int maxX = points.Max(p => p.X);
            int maxY = points.Max(p => p.Y);

            var lowerBounds = new[] { minX - (maxX - minX + 1), minY - (maxY - minY + 1) };
            var lengths = new[] { (maxX - minX + 1) * 3, (maxY - minY + 1) * 3 };
            var grid = (int[,])Array.CreateInstance(typeof(int), lengths, lowerBounds);

            return grid;
        }

        private List<Point> ParsePoints(string input)
        {
            var points = new List<Point>();

            using (var reader = new StringReader(input))
            {
                string line;
                int id = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    var components = line.Split(',');
                    if (components.Length != 2) throw new Exception();
                    var x = int.Parse(components[0].Trim());
                    var y = int.Parse(components[1].Trim());
                    var point = new Point
                    {
                        Id = id,
                        X = x,
                        Y = y
                    };
                    points.Add(point);
                    id++;
                }
            }

            return points;
        }

        private class Point
        {
            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
