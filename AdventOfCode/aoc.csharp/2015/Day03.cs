using aoc.csharp.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2015;

public class Day03 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var text = input.ReadToEnd();
        var part1 = DeliverPresents(text, 1);
        var part2 = DeliverPresents(text, 2);
        return (part1.ToString(), part2.ToString());
    }

    public static int DeliverPresents(string input, int santas)
    {
        var houses = new HashSet<Point2D>();
        var locations = new Point2D[santas];
        foreach (var i in Enumerable.Range(0, santas))
        {
            locations[i] = new Point2D(0, 0);
        }
        int turn = 0;

        houses.Add(locations[0]);

        foreach (var direction in input)
        {
            var location = locations[turn];

            if (direction == '>')
            {
                location = new Point2D(location.X + 1, location.Y);
            }
            else if (direction == 'v')
            {
                location = new Point2D(location.X, location.Y - 1);
            }
            else if (direction == '<')
            {
                location = new Point2D(location.X - 1, location.Y);
            }
            else if (direction == '^')
            {
                location = new Point2D(location.X, location.Y + 1);
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
}
