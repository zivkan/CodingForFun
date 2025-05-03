using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc.csharp._2019;

public class Day08 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        const int width = 25;
        const int height = 6;

        List<char[]> layers = GetLayers(width, height, input);

        var fewestZeros = layers.Select(l => new KeyValuePair<int, char[]>(l.Count(c => c == '0'), l))
            .Smallest(l => l.Key)
            .Value;

        var part1 = fewestZeros.Count(c => c == '1') * fewestZeros.Count(c => c == '2');

        var flattened = FlattenLayers(layers);

        var part2 = ToOutput(width, flattened);

        return (part1.ToString(), part2);
    }

    private static string ToOutput(int width, char[] flattened)
    {
        var sb = new StringBuilder();

        int col = 0;
        foreach (var c in flattened)
        {
            if (col == 0 && sb.Length > 0)
            {
                sb.AppendLine();
            }

            if (c == '0')
            {
                sb.Append(' ');
            }
            else if (c == '1')
            {
                sb.Append('#');
            }
            else
            {
                throw new Exception();
            }
            col++;
            if (col == width)
            {
                col = 0;
            }
        }

        return sb.ToString();
    }

    public static List<char[]> GetLayers(int width, int height, TextReader input)
    {
        var layers = new List<char[]>();

        char[]? buffer;
        do
        {
            buffer = new char[width * height];
            int read;
            if ((read = input.Read(buffer, 0, buffer.Length)) < buffer.Length)
            {
                buffer = null;
            }
            else
            {
                layers.Add(buffer);
            }
        } while (buffer != null);

        return layers;
    }

    public static char[] FlattenLayers(List<char[]> layers)
    {
        var result = new char[layers[0].Length];
        var hitmap = new bool[layers[0].Length];

        for (int l = 0; l < layers.Count; l++)
        {
            var layer = layers[l];
            for (int pixel = 0; pixel < layer.Length; pixel++)
            {
                if (hitmap[pixel] == false && layer[pixel] != '2')
                {
                    hitmap[pixel] = true;
                    result[pixel] = layer[pixel];
                }
            }
        }

        return result;
    }
}
