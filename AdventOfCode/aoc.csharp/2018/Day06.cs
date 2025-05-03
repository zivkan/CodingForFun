using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2018;

public class Day06 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var text = input.ReadToEnd();
        var part1 = FindLargestArea(text);
        var part2 = SizeOfTargetArea(text, 10000);
        return (part1.ToString(), part2.ToString());
    }

    public static int FindLargestArea(string input)
    {
        var points = ParsePoints(input);
        var grid = CreateGrid(points);
        FillGridWithClosestId(grid, points);
        var areas = GetAreaPerId(grid);
        return areas.Max(a => a.Value);
    }

    public static int SizeOfTargetArea(string input, int threshold)
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

    private static Dictionary<int, int> GetAreaPerId(int[,] grid)
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

    private static void FillGridWithClosestId(int[,] grid, List<Point> points)
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

    private static void FillGridWithTotalManhattenDistance(int[,] grid, List<Point> points)
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

    private static int[,] CreateGrid(List<Point> points)
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

    private static List<Point> ParsePoints(string input)
    {
        var points = new List<Point>();

        using (var reader = new StringReader(input))
        {
            string? line;
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
