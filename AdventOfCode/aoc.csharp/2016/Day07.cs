using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2016;

public class Day07 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        int part1 = 0, part2 = 0;
        string? line;
        while ((line = input.ReadLine()) != null)
        {
            if (SupportsTls(line))
            {
                part1++;
            }
            if (SupportsSsl(line))
            {
                part2++;
            }
        }

        return (part1.ToString(), part2.ToString());
    }

    public static bool SupportsTls(string input)
    {
        var parsed = ParseIp(input);
        var ips = parsed.Item1;
        var hypernets = parsed.Item2;

        return ContainsAbba(ips) && !ContainsAbba(hypernets);
    }

    private static Tuple<List<string>, List<string>> ParseIp(string input)
    {
        var ips = new List<string>();
        var hypernets = new List<string>();

        int startIndex = input.IndexOf("[", StringComparison.Ordinal);
        int endIndex = -1;
        while (startIndex > 0)
        {
            ips.Add(input.Substring(endIndex + 1, startIndex - endIndex - 1));

            endIndex = input.IndexOf("]", startIndex + 1, StringComparison.Ordinal);
            hypernets.Add(input.Substring(startIndex + 1, endIndex - startIndex - 1));

            startIndex = input.IndexOf("[", endIndex + 1, StringComparison.Ordinal);
        }
        ips.Add(input.Substring(endIndex + 1));

        Tuple<List<string>, List<string>> parsed = new Tuple<List<string>, List<string>>(ips, hypernets);
        return parsed;
    }

    private static bool ContainsAbba(List<string> inputs)
    {
        foreach (var input in inputs)
        {
            for (int i = 0; i < input.Length - 3; i++)
            {
                char c1 = input[i];
                char c2 = input[i + 1];
                char c3 = input[i + 2];
                char c4 = input[i + 3];
                if (c1 != c2 && c1 == c4 && c2 == c3)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool SupportsSsl(string input)
    {
        var parsed = ParseIp(input);
        var aba = FindAbaCandidates(parsed.Item1);
        var bab = FindAbaCandidates(parsed.Item2);
        foreach (var candidate in aba)
        {
            var toFind = new string(new[] { candidate[1], candidate[0], candidate[1] });
            if (bab.Contains(toFind))
            {
                return true;
            }
        }
        return false;
    }

    private static List<string> FindAbaCandidates(List<string> parsed)
    {
        var list = new List<string>();
        foreach (var part in parsed)
        {
            for (int i = 0; i < part.Length - 2; i++)
            {
                if (part[i] == part[i + 2] && part[i] != part[i + 1])
                {
                    list.Add(part.Substring(i, 3));
                }
            }
        }

        return list;
    }
}
