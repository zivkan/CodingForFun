using System;
using System.IO;

namespace aoc.csharp._2017;

public class Day17 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var text = input.ReadToEnd();
        var buffer = SpinLock(2017, int.Parse(text));
        var part1 = ValueAfter(buffer, 2017);

        int part2 = -1;
        int current = 0;
        for (int i = 1; i <= 50000000; i++)
        {
            current = (current + 371) % i + 1;
            if (current == 1)
            {
                part2 = i;
            }
        }

        return (part1.ToString(), part2.ToString());
    }

    public static int[] SpinLock(int iterations, int spins)
    {
        int[] buffer = new int[iterations + 1];
        buffer[0] = 0;

        int current = 0;

        for (int i = 1; i <= iterations; i++)
        {
            current = (current + spins) % i;

            int toCopy = i - current - 1;
            if (toCopy > 0)
            {
                Array.Copy(buffer, current + 1, buffer, current + 2, i - current - 1);
            }

            current++;
            buffer[current] = i;
        }

        return buffer;
    }

    public static int ValueAfter(int[] buffer, int value)
    {
        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] == value)
            {
                return buffer[(i + 1) % buffer.Length];
            }
        }

        throw new Exception();
    }
}
