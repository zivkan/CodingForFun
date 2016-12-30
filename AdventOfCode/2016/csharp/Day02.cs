using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day02
    {
        private const string Keypad1Layout = "123\n" +
                                            "456\n" +
                                            "789\n";
        private const string Keypad2Layout = "  1  \n" +
                                             " 234 \n" +
                                             "56789\n" +
                                             " ABC \n" +
                                             "  D  \n";
        private const char InvalidKey = ' ';

        private const string SampleInput = "ULL\nRRDDD\nLURDL\nUUUUD";

        // coordinate system is that top left is origin, X increases to the right and Y increases down.
        private static readonly Position Up = new Position(0, -1);
        private static readonly Position Down = new Position(0, 1);
        private static readonly Position Right = new Position(1, 0);
        private static readonly Position Left = new Position(-1, 0);

        private readonly ITestOutputHelper _output;

        public Day02(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Part1Sample()
        {
            string expected = "1985";

            var code = GetCodeWithKeypad1(SampleInput);

            Assert.Equal(expected, code);
        }

        [Fact]
        public void Part1()
        {
            var input = GetPuzzleInput.Day(2).ReadToEnd();
            var code = GetCodeWithKeypad1(input);
            _output.WriteLine(code);
        }

        [Fact]
        public void Part2Sample()
        {
            string expected = "5DB3";

            var code = GetCodeWithKeypad2(SampleInput);

            Assert.Equal(expected, code);
        }

        [Fact]
        public void Part2()
        {
            var input = GetPuzzleInput.Day(2).ReadToEnd();
            var code = GetCodeWithKeypad2(input);
            _output.WriteLine(code);
        }

        private string GetCodeWithKeypad1(string input)
        {
            var keypad = GetKeypad(Keypad1Layout);
            return GetCode(input, keypad);
        }

        private string GetCodeWithKeypad2(string input)
        {
            var keypad = GetKeypad(Keypad2Layout);
            return GetCode(input, keypad);
        }

        private string GetCode(string input, IDictionary<Position, char> keypad)
        {
            var inputLines = GetInputLines(input);
            return GetCode(inputLines, keypad);
        }

        private string GetCode(List<string> inputLines, IDictionary<Position, char> keypad)
        {
            var position = GetPositionOf5(keypad);
            var code = new StringBuilder(inputLines.Count);

            foreach (var line in inputLines)
            {
                foreach (var c in line)
                {
                    var direction = GetDirection(c);
                    var newPosition = position + direction;
                    if (keypad.ContainsKey(newPosition))
                    {
                        position = newPosition;
                    }
                }

                code.Append(keypad[position]);
            }

            return code.ToString();
        }

        private Position GetDirection(char c)
        {
            switch (c)
            {
                case 'U':
                case 'u':
                    return Up;

                case 'D':
                case 'd':
                    return Down;

                case 'R':
                case 'r':
                    return Right;

                case 'L':
                case 'l':
                    return Left;

                default:
                    throw new ArgumentException("Invalid direction");
            }
        }

        private Position GetPositionOf5(IDictionary<Position, char> keypad)
        {
            return keypad.Where(k => k.Value == '5').Select(k => k.Key).Single();
        }

        private IDictionary<Position, char> GetKeypad(string keypadLayout)
        {
            var lines = GetInputLines(keypadLayout);
            var chars = GetKeypadChars(lines);
            return GetKeypadDictionary(chars);
        }

        private IDictionary<Position, char> GetKeypadDictionary(char[,] chars)
        {
            var dictionary = new Dictionary<Position, char>();

            int width = chars.GetLength(0);
            int height = chars.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char c = chars[x, y];
                    if (c != InvalidKey)
                    {
                        Position pos = new Position(x, y);
                        dictionary.Add(pos, c);
                    }
                }
            }

            return dictionary;
        }

        private char[,] GetKeypadChars(List<string> lines)
        {
            int width = lines[0].Length;
            int height = lines.Count;

            char[,] chars = new char[width, height];

            for (int y = 0; y < height; y++)
            {
                if (lines[y].Length != width)
                {
                    throw new ArgumentException("Not all lines are the same length");
                }

                for (int x = 0; x < width; x++)
                {
                    chars[x, y] = lines[y][x];
                }
            }

            return chars;
        }

        private static List<string> GetInputLines(string input)
        {
            List<string> lines = new List<string>();
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        private class Position
        {
            public int X { get; }
            public int Y { get; }

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static Position operator + (Position a, Position b)
            {
                return new Position(a.X + b.X, a.Y + b.Y);
            }

            public override bool Equals(object obj)
            {
                Position other = obj as Position;

                if (other == null)
                {
                    return false;
                }

                return X == other.X && Y == other.Y;
            }

            public override int GetHashCode()
            {
                return X.GetHashCode() ^ Y.GetHashCode();
            }
        }
    }
}
