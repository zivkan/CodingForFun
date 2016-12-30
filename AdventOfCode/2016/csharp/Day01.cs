using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day01
    {
        private readonly ITestOutputHelper _output;

        public Day01(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData("R2, L3", 5)]
        [InlineData("R2, R2, R2", 2)]
        [InlineData("R5, L5, R5, R3", 12)]
        public void Part1Sample(string input, int expected)
        {
            var actual = GetDistanceFromOrigin(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Part1()
        {
            var input = GetPuzzleInput.Day(1).ReadToEnd();
            var distance = GetDistanceFromOrigin(input);
            _output.WriteLine("{0}", distance);
        }

        [Theory]
        [InlineData("R8, R4, R4, R8", 4)]
        public void Part2Sample(string input, int expected)
        {
            var distance = GetDistanceFromOriginOfFirstRepeatedLocation(input);

            Assert.Equal(expected, distance);
        }

        [Fact]
        public void Part2()
        {
            var input = GetPuzzleInput.Day(1).ReadToEnd();
            int distance = GetDistanceFromOriginOfFirstRepeatedLocation(input);
            _output.WriteLine("{0}", distance);
        }

        private int GetDistanceFromOrigin(string input)
        {
            var instructions = ParseInstructions(input);
            var path = GetPath(instructions);
            var finalLocation = path.Last();
            int actual = GetDistanceFromOrigin(finalLocation);
            return actual;
        }

        private int GetDistanceFromOrigin(Vector2D location)
        {
            return Math.Abs(location.X) + Math.Abs(location.Y);
        }

        private List<Instruction> ParseInstructions(string input)
        {
            Regex regex = new Regex("[RL][0-9]+");
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

        private List<Vector2D> GetPath(IEnumerable<Instruction> instructions)
        {
            Vector2D facing = new Vector2D(0, 1);
            Vector2D location = new Vector2D(0, 0);
            List<Vector2D> path = new List<Vector2D> {location};

            foreach (var instruction in instructions)
            {
                int turn = instruction.Direction.Equals('R') ? 1 : -1;
                facing = new Vector2D(turn*facing.Y, -turn*facing.X);
                for (int distance = 0; distance < instruction.Distance; distance++)
                {
                    location = new Vector2D(location.X + facing.X, location.Y + facing.Y);
                    path.Add(location);
                }
            }

            return path;
        }

        private int GetDistanceFromOriginOfFirstRepeatedLocation(string input)
        {
            var instructions = ParseInstructions(input);
            var path = GetPath(instructions);
            var firstRepeat = GetFirstRepeat(path);
            var distance = GetDistanceFromOrigin(firstRepeat);
            return distance;
        }

        private Vector2D GetFirstRepeat(IEnumerable<Vector2D> path)
        {
            List<Vector2D> visited = new List<Vector2D>();

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

        private class Vector2D
        {
            public Vector2D(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }
    }
}
