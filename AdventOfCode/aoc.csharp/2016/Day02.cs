using aoc.csharp.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc.csharp._2016;

public class Day02 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var text = Input.GetLines(input);

        string part1 = GetCodeWithKeypad1(text);
        string part2 = GetCodeWithKeypad2(text);

        return (part1, part2);
    }

    private static readonly IReadOnlyList<string> Keypad1Layout = new List<string>()
    {
        "123",
        "456",
        "789"
    };
    private static readonly IReadOnlyList<string> Keypad2Layout = new List<string>()
    {
        "  1  ",
        " 234 ",
        "56789",
        " ABC ",
        "  D  "
    };
    private const char InvalidKey = ' ';

    // coordinate system is that top left is origin, X increases to the right and Y increases down.
    private static readonly Point2D Up = new Point2D(0, -1);
    private static readonly Point2D Down = new Point2D(0, 1);
    private static readonly Point2D Right = new Point2D(1, 0);
    private static readonly Point2D Left = new Point2D(-1, 0);

    public static string GetCodeWithKeypad1(IReadOnlyList<string> input)
    {
        var keypad = GetKeypad(Keypad1Layout);
        return GetCode(input, keypad);
    }

    public static string GetCodeWithKeypad2(IReadOnlyList<string> input)
    {
        var keypad = GetKeypad(Keypad2Layout);
        return GetCode(input, keypad);
    }

    private static string GetCode(IReadOnlyList<string> inputLines, IDictionary<Point2D, char> keypad)
    {
        var Point2D = GetLocationOf5(keypad);
        var code = new StringBuilder(inputLines.Count);

        foreach (var line in inputLines)
        {
            foreach (var c in line)
            {
                var direction = GetDirection(c);
                var newPoint2D = Point2D + direction;
                if (keypad.ContainsKey(newPoint2D))
                {
                    Point2D = newPoint2D;
                }
            }

            code.Append(keypad[Point2D]);
        }

        return code.ToString();
    }

    private static Point2D GetDirection(char c)
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

    private static Point2D GetLocationOf5(IDictionary<Point2D, char> keypad)
    {
        return keypad.Where(k => k.Value == '5').Select(k => k.Key).Single();
    }

    private static IDictionary<Point2D, char> GetKeypad(IReadOnlyList<string> keypadLayout)
    {
        var chars = GetKeypadChars(keypadLayout);
        return GetKeypadDictionary(chars);
    }

    private static IDictionary<Point2D, char> GetKeypadDictionary(char[,] chars)
    {
        var dictionary = new Dictionary<Point2D, char>();

        int width = chars.GetLength(0);
        int height = chars.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char c = chars[x, y];
                if (c != InvalidKey)
                {
                    Point2D pos = new Point2D(x, y);
                    dictionary.Add(pos, c);
                }
            }
        }

        return dictionary;
    }

    private static char[,] GetKeypadChars(IReadOnlyList<string> lines)
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
}
