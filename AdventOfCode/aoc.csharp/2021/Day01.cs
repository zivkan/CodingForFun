using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2021;

public class Day01 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var depths = Input.ToList<int>(input);

        var part1 = Part1(depths);
        var part2 = Part2(depths);

        return (part1.ToString(), part2.ToString());
    }

    public static int Part1(IReadOnlyList<int> input)
    {
        int increases = 0;

        for (int i = 1; i < input.Count; i++)
        {
            if (input[i] > input[i-1])
            {
                increases++;
            }
        }

        return increases;
    }

    public static int Part2(IReadOnlyList<int> input)
    {
        int increases = 0;

        for (int i = 3; i < input.Count; i++)
        {
            var prev = input.Skip(i - 3).Take(3).Sum();
            var curr = input.Skip(i - 2).Take(3).Sum();

            if (curr > prev)
            {
                increases++;
            }
        }

        return increases;
    }
}
