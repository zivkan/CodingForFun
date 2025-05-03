using System.IO;

namespace aoc.csharp._2015;

public class Day01 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        string text = input.ReadToEnd();
        int floor = 0;
        int? firstBasementIndex = null;
        for (var i = 0; i < text.Length; i++)
        {
            var c = text[i];
            if (c == '(')
            {
                floor++;
            }
            else
            {
                floor--;
            }
            if (firstBasementIndex == null && floor == -1)
            {
                firstBasementIndex = i + 1;
            }
        }

        var part1 = floor.ToString();
        var part2 = firstBasementIndex?.ToString() ?? string.Empty;
        return (part1, part2);
    }
}
