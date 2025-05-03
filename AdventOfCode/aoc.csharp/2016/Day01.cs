using aoc.csharp.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2016;

public class Day01 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var text = input.ReadToEnd();
        uint part1 = GetDistanceFromOrigin(text);
        uint part2 = GetDistanceFromOriginOfFirstRepeatedLocation(text);
        return (part1.ToString(), part2.ToString());
    }

    public static uint GetDistanceFromOrigin(string input)
    {
        List<Instruction> instructions = ParseInstructions(input);
        List<Point2D> path = GetPath(instructions);
        Point2D finalLocation = path.Last();
        uint actual = finalLocation.GetManhattenDistance();
        return actual;
    }

    public static uint GetDistanceFromOriginOfFirstRepeatedLocation(string input)
    {
        List<Instruction> instructions = ParseInstructions(input);
        List<Point2D> path = GetPath(instructions);
        Point2D firstRepeat = GetFirstRepeat(path);
        uint distance = firstRepeat.GetManhattenDistance();
        return distance;
    }

    private static List<Instruction> ParseInstructions(string input)
    {
        var regex = new Regex("[RL][0-9]+");
        var matches = regex.Matches(input);
        var instructions = new List<Instruction>(matches.Count);
        for (int i = 0; i < matches.Count; i++)
        {
            string str = matches[i].Value;
            Instruction instruction = new Instruction(str[0], int.Parse(str.Substring(1)));
            instructions.Add(instruction);
        }

        return instructions;
    }

    private static List<Point2D> GetPath(IEnumerable<Instruction> instructions)
    {
        var facing = new Point2D(0, 1);
        var location = new Point2D(0, 0);
        var path = new List<Point2D> { location };

        foreach (var instruction in instructions)
        {
            int turn = instruction.Direction.Equals('R') ? 1 : -1;
            facing = new Point2D(turn * facing.Y, -turn * facing.X);
            for (int distance = 0; distance < instruction.Distance; distance++)
            {
                location = new Point2D(location.X + facing.X, location.Y + facing.Y);
                path.Add(location);
            }
        }

        return path;
    }

    private static Point2D GetFirstRepeat(IEnumerable<Point2D> path)
    {
        var visited = new List<Point2D>();

        foreach (var location in path)
        {
            if (visited.Any(v => v.X == location.X && v.Y == location.Y))
            {
                return location;
            }
            else
            {
                visited.Add(location);
            }
        }

        throw new Exception("Never repeated any location");
    }

    private class Instruction
    {
        public Instruction(char direction, int distance)
        {
            Direction = direction;
            Distance = distance;
        }

        public char Direction { get; }
        public int Distance { get; }
    }
}
