using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace aoc.csharp._2015;

public class Day06 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        string[]? lines = Input.GetLines(input);
        Regex regex = new(@"^(?<opcode>turn off|turn on|toggle) (?<startx>\d+),(?<starty>\d+) through (?<endx>\d+),(?<endy>\d+)$");
        BitArray part1 = new(1000 * 1000, false);
        uint[][] part2 = new uint[1000][];
        for (int i = 0; i < part2.Length; i++)
        {
            part2[i] = new uint[1000];
        }

        foreach (var line in lines)
        {
            var match = regex.Match(line);
            if (!match.Success)
            {
                throw new Exception("Error parsing line: " + line);
            }

            Action<BitArray, uint[][], int, int> action = match.Groups["opcode"].Value switch
            {
                "turn on" => TurnOn,
                "turn off" => TurnOff,
                "toggle" => Toggle,
                _ => throw new Exception("Unexpected instruction " + match.Groups["action"].Value)
            };

            var startx = int.Parse(match.Groups["startx"].Value);
            var starty = int.Parse(match.Groups["starty"].Value);
            var endx = int.Parse(match.Groups["endx"].Value);
            var endy = int.Parse(match.Groups["endy"].Value);

            for (int x = startx; x <= endx; x++)
            {
                for (int y = starty; y <= endy; y++)
                {
                    action(part1, part2, x, y);
                }
            }
        }

        int part1Answer = 0;
        long part2Answer = 0;
        for (int x = 0; x < 1000; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                int index = GetPart1Index(x, y);
                if (part1.Get(index))
                {
                    part1Answer++;
                }

                part2Answer += part2[x][y];
            }
        }

        return (part1Answer.ToString(), part2Answer.ToString());
    }

    private static void TurnOn(BitArray part1, uint[][] part2, int x, int y)
    {
        int index = GetPart1Index(x, y);
        part1.Set(index, true);

        part2[x][y] += 1;
    }

    private static void TurnOff(BitArray part1, uint[][] part2, int x, int y)
    {
        int index = GetPart1Index(x, y);
        part1.Set(index, false);

        var value = part2[x][y];
        if (value != 0)
        {
            part2[x][y] = value - 1;
        }
    }

    private static void Toggle(BitArray part1, uint[][] part2, int x, int y)
    {
        int index = GetPart1Index(x, y);
        var value = !part1.Get(index);
        part1.Set(index, value);

        part2[x][y] += 2;
    }

    private static int GetPart1Index(int x, int y)
    {
        return x + y * 1000;
    }
}
