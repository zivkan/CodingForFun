using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2017;

public class Day02 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var text = input.ReadToEnd();

        var line = new List<int>();
        var lines = new List<int[]>();
        int current = 0;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '\n' || text[i] == '\r')
            {
                if (text[i] == '\r' && i + 1 < text.Length && text[i + 1] == '\n')
                {
                    i++;
                }
                line.Add(current);
                current = 0;
                lines.Add(line.ToArray());
                line.Clear();
            }
            else if (text[i] == '\t')
            {
                line.Add(current);
                current = 0;
            }
            else
            {
                current = current * 10 + (text[i] - '0');
            }
        }

        var data = lines.ToArray();

        int part1 = CalcPart1(data);
        int part2 = CalcPart2(data);
        return (part1.ToString(), part2.ToString());
    }

    public static int CalcPart1(int[][] input)
    {
        int checksum = 0;
        for (int row = 0; row < input.Length; row++)
        {
            int[] line = input[row];
            int min = line[0];
            int max = min;
            for (int col = 1; col < line.Length; col++)
            {
                int num = line[col];
                if (num > max)
                {
                    max = num;
                }
                if (num < min)
                {
                    min = num;
                }
            }

            checksum += max - min;
        }

        return checksum;
    }

    public static int CalcPart2(int[][] input)
    {
        int checksum = 0;
        for (int row = 0; row < input.Length; row++)
        {
            int[] line = input[row];
            for (int i = 0; i < line.Length; i++)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if ((line[i] % line[j]) == 0)
                    {
                        checksum += line[i] / line[j];
                    }
                }
            }
        }

        return checksum;
    }
}
