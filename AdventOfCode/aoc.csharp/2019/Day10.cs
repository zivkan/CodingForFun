using aoc.csharp.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2019
{
    public class Day10 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var map = Input.GetLines(input);
            var asteroids = GetAsteroids(map);

            int maxVisible = 0;

            List<AsteroidInfo>? baseLayout = null;

            foreach (var asteroid in asteroids)
            {
                var asteroidInfos = GetAsteroidInfos(asteroids, asteroid);
                var visible = GetVisibleAsteroidCount(asteroidInfos);
                if (visible > maxVisible)
                {
                    maxVisible = visible;
                    baseLayout = asteroidInfos;
                }
            }

            Point2D? target200 = null;
            if (baseLayout != null)
            {
                var targets = GetLaserTargets(baseLayout);
                target200 = targets.Skip(199).Take(1).Single();
            }

            return (maxVisible.ToString(), (target200.X * 100 + target200.Y).ToString());
        }

        public static Point2D GetDirection(Point2D vector)
        {
            var end = Math.Min(Math.Abs(vector.X), Math.Abs(vector.Y));
            if (end == 0)
            {
                end = Math.Max(Math.Abs(vector.X), Math.Abs(vector.Y));
            }

            for (int i = end; i > 1; i--)
            {
                if (vector.X % i == 0 && vector.Y % i == 0)
                {
                    return new Point2D(vector.X / i, vector.Y / i);
                }
            }

            return vector;
        }

        public static List<Point2D> GetAsteroids(string[] map)
        {
            if (map == null || map.Length == 0)
            {
                throw new ArgumentException("Map must be provided with at least one line");
            }

            var asteroids = new List<Point2D>();
            var width = map[0].Length;

            for (int y = 0; y < map.Length; y++)
            {
                var line = map[y];
                if (y > 0 && line.Length != width)
                {
                    throw new ArgumentException("Map is not rectangular");
                }

                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                    {
                        asteroids.Add(new Point2D(x, y));
                    }
                }
            }

            return asteroids;
        }

        public static List<AsteroidInfo> GetAsteroidInfos(List<Point2D> asteroids, Point2D origin)
        {
            var result = new List<AsteroidInfo>(asteroids.Count - 1);

            foreach (var asteroid in asteroids)
            {
                if (asteroid == origin)
                {
                    continue;
                }

                var vector = asteroid - origin;
                var direction = GetDirection(vector);

                var asteroidInfo = new AsteroidInfo(asteroid, vector, direction);
                result.Add(asteroidInfo);
            }

            return result;
        }

        public static int GetVisibleAsteroidCount(List<AsteroidInfo> asteroids)
        {
            return asteroids
                .Select(a => a.Direction)
                .Distinct()
                .Count();
        }

        public static IEnumerable<Point2D> GetLaserTargets(List<AsteroidInfo> asteroids)
        {
            var targets = asteroids
                .GroupBy(a => a.Direction)
                .Select(g =>
                {
                    var angle = Math.Atan2(g.Key.Y, g.Key.X) + Math.PI / 2.0;
                    while (angle < 0.0)
                    {
                        angle += Math.PI*2;
                    }
                    return new
                    {
                        Angle = angle,
                        Asteroids = g.OrderByDescending(a => a.Vector.GetManhattenDistance()).Select(a => a.Position).ToList()
                    };
                })
                .OrderBy(g => g.Angle)
                .Select(g => g.Asteroids)
                .ToList();

            int i = 0;
            while (targets.Count > 0)
            {
                var targetDirection = targets[i];
                var asteroid = targetDirection[targetDirection.Count - 1];
                yield return asteroid;
                targetDirection.RemoveAt(targetDirection.Count - 1);

                if (targetDirection.Count == 0)
                {
                    targets.RemoveAt(i);
                }
                else
                {
                    i++;
                }
                if (i == targets.Count)
                {
                    i = 0;
                }
            }
        }

        public class AsteroidInfo
        {
            public Point2D Position { get; }
            public Point2D Vector { get; }
            public Point2D Direction { get; }

            public AsteroidInfo(Point2D position, Point2D vector, Point2D direction)
            {
                Position = position;
                Vector = vector;
                Direction = direction;
            }
        }
    }
}
