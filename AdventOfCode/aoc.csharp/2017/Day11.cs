using System;
using System.IO;

namespace aoc.csharp._2017
{
    public class Day11 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var (last, max) = GetDistances(text);
            return (last.ToString(), max.ToString());
        }

        private static int GetDistance(int x, int y, int z)
        {
            return (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;
        }

        public static (int last, int max) GetDistances(string input)
        {
            int x = 0, y = 0, z = 0;
            int distance = 0;
            int maxDistance = 0;

            var directions = input.Split(',');

            foreach (var direction in directions)
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
