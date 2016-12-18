using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day8
    {
        private readonly ITestOutputHelper _output;

        private static readonly Regex RectRegex = new Regex("^rect ([\\d]+)x([\\d]+)$");
        private static readonly Regex RotateColumnRegex = new Regex("^rotate column x=([\\d]+) by ([\\d]+)$");
        private static readonly Regex RotateRowRegex = new Regex("^rotate row y=([\\d]+) by ([\\d]+)$");

        public Day8(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void SampleData()
        {
            string input = "rect 3x2\n" +
                           "rotate column x=1 by 1\n" +
                           "rotate row y=0 by 4\n" +
                           "rotate column x=1 by 1\n";
            List<IInstruction> instructions;
            using (var reader = new StringReader(input))
            {
                instructions = ParseInstructions(reader);
            }

            var screen = new bool[7, 3];
            InitialiseScreen(screen);

            foreach (var instruction in instructions)
            {
                instruction.Execute(screen);
            }

            DisplayScreen(screen);
        }

        [Fact]
        public void RealData()
        {
            List<IInstruction> instructions;
            using (var reader = GetPuzzleInput.Day(8))
            {
                instructions = ParseInstructions(reader);
            }

            var screen = new bool[50, 6];
            InitialiseScreen(screen);

            foreach (var instruction in instructions)
            {
                instruction.Execute(screen);
            }

            _output.WriteLine("Number of lights on {0}", CountOnLights(screen));

            DisplayScreen(screen);
        }

        private int CountOnLights(bool[,] screen)
        {
            int count = 0;
            int width = screen.GetLength(0);
            int height = screen.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (screen[x, y])
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void DisplayScreen(bool[,] screen)
        {
            int width = screen.GetLength(0);
            int height = screen.GetLength(1);
            char[] row = new char[width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    row[x] = screen[x, y] ? '#' : ' ';
                }
                _output.WriteLine(new string(row));
            }
        }

        private void InitialiseScreen(bool[,] screen)
        {
            int width = screen.GetLength(0);
            int height = screen.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    screen[x, y] = false;
                }
            }
        }

        private List<IInstruction> ParseInstructions(TextReader input)
        {
            var instructions = new List<IInstruction>();
            string line;

            while ((line = input.ReadLine()) != null)
            {
                Match match;
                if ((match = RectRegex.Match(line)).Success)
                {
                    int width = int.Parse(match.Groups[1].Value);
                    int height = int.Parse(match.Groups[2].Value);
                    instructions.Add(new Rect(width, height));
                }
                else if ((match = RotateColumnRegex.Match(line)).Success)
                {
                    int column = int.Parse(match.Groups[1].Value);
                    int amount = int.Parse(match.Groups[2].Value);
                    instructions.Add(new RotateColumn(column, amount));
                }
                else if ((match = RotateRowRegex.Match(line)).Success)
                {
                    int row = int.Parse(match.Groups[1].Value);
                    int amount = int.Parse(match.Groups[2].Value);
                    instructions.Add(new RotateRow(row, amount));
                }
                else
                {
                    throw new ArgumentException("Can't parse line");
                }
            }

            return instructions;
        }

        private interface IInstruction
        {
            void Execute(bool[,] screen);
        }

        private class Rect : IInstruction
        {
            public int Width { get; }
            public int Height { get; }

            public Rect(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return $"rect {Width}x{Height}";
            }

            public void Execute(bool[,] screen)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        screen[x, y] = true;
                    }
                }
            }
        }

        private class RotateColumn : IInstruction
        {
            public int Column { get; }
            public int Amount { get; }

            public RotateColumn(int column, int amount)
            {
                Column = column;
                Amount = amount;
            }

            public override string ToString()
            {
                return $"rotate column x={Column} by {Amount}";
            }

            public void Execute(bool[,] screen)
            {
                var height = screen.GetLength(1);
                var column = new bool[height];

                for (int y = 0; y < height; y++)
                {
                    column[y] = screen[Column, y];
                }

                for (int y = 0; y < height; y++)
                {
                    screen[Column, (y + Amount)%height] = column[y];
                }
            }
        }

        private class RotateRow : IInstruction
        {
            public int Row { get; }
            public int Amount { get; }

            public RotateRow(int row, int amount)
            {
                Row = row;
                Amount = amount;
            }

            public override string ToString()
            {
                return $"rotate row y={Row} by {Amount}";
            }

            public void Execute(bool[,] screen)
            {
                var width = screen.GetLength(0);
                var row = new bool[width];

                for (int x = 0; x < width; x++)
                {
                    row[x] = screen[x, Row];
                }

                for (int x = 0; x < width; x++)
                {
                    screen[(x + Amount)%width, Row] = row[x];
                }
            }
        }
    }
}
