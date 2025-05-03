using aoc.csharp.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2018;

public class Day03 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var text = input.ReadToEnd();
        var (overlaps, intact) = CountOverlappedSquares(text);
        return (overlaps.ToString(), intact.ToString());
    }

    private static readonly Regex _regex = new Regex(@"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<w>\d+)x(?<h>\d+)");

    public static (int overlaps, int intact) CountOverlappedSquares(string input)
    {
        var matches = _regex.Matches(input);
        var points = new Dictionary<Point2D, List<int>>();
        var intactClaims = new HashSet<int>();
        foreach (Match? match in matches)
        {
            if (match == null) throw new Exception();
            var id = int.Parse(match.Groups["id"].Value);
            intactClaims.Add(id);
            var left = int.Parse(match.Groups["x"].Value);
            var top = int.Parse(match.Groups["y"].Value);
            var right = left + int.Parse(match.Groups["w"].Value);
            var bottom = top + int.Parse(match.Groups["h"].Value);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    var p = new Point2D(x, y);
                    if (points.TryGetValue(p, out List<int>? ids))
                    {
                        ids.Add(id);
                        intactClaims.Remove(id);
                        intactClaims.Remove(ids[0]);
                    }
                    else
                    {
                        ids = new List<int>
                        {
                            id
                        };
                        points[p] = ids;
                    }
                }
            }
        }

        return (points.Count(p => p.Value.Count > 1), intactClaims.Single());
    }
}
