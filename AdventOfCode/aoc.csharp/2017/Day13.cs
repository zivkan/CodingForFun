using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2017;

public class Day13 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var text = input.ReadToEnd();
        var scanners = ParseFirewall(text);

        var severity = EnterFirewall(scanners, 0);
        var firstSafeDelay = FindFirstSafeDelay(scanners);

        return (severity?.ToString() ?? "(null)", firstSafeDelay.ToString());
    }

    public static int? EnterFirewall(List<(int, int)> scanners, int delay)
    {
        int severity = 0;
        bool caught = false;
        foreach (var (depth, range) in scanners)
        {
            if ((depth + delay) % (2 * range - 2) == 0)
            {
                severity += depth * range;
                caught = true;
            }
        }

        return caught ? (int?)severity : null;
    }

    public static List<(int depth, int range)> ParseFirewall(string input)
    {
        List<(int depth, int range)> scanners = new List<(int depth, int range)>();
        using (var reader = new StringReader(input))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                int index = line.IndexOf(": ");
                string str = line.Substring(0, index);
                int depth = int.Parse(str);
                str = line.Substring(index + 2);
                int range = int.Parse(str);
                scanners.Add((depth, range));
            }
        }
        return scanners;
    }

    public static int FindFirstSafeDelay(List<(int, int)> scanners)
    {
        for (int i = 0; i >= 0; i++)
        {
            if (EnterFirewall(scanners, i) == null)
            {
                return i;
            }
        }

        throw new ArgumentException("Solution couldn't be found for provided input");
    }
}
