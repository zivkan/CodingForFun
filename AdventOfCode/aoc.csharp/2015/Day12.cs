using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace aoc.csharp._2015;

public class Day12 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var bytes = Encoding.UTF8.GetBytes(input.ReadToEnd());

        var part1 = Part1(bytes);
        var part2 = Part2(bytes);

        return (part1.ToString(), part2.ToString());
    }

    public static int Part1(byte[] json)
    {
        var reader = new Utf8JsonReader(json);

        int sum = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                var value = reader.GetInt32();
                sum += value;
            }
        }

        return sum;
    }

    public static int Part2(byte[] json)
    {
        var reader = new Utf8JsonReader(json);

        int sum = 0;
        Stack<int> stack = new();
        JsonTokenType prevTokenType = JsonTokenType.None;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                var value = reader.GetInt32();
                sum += value;
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                stack.Push(sum);
                sum = 0;
            }
            else if (reader.TokenType == JsonTokenType.EndObject)
            {
                sum += stack.Pop();
            }
            else if (reader.TokenType == JsonTokenType.String && prevTokenType == JsonTokenType.PropertyName)
            {
                if (reader.ValueTextEquals("red"))
                {
                    var depth = reader.CurrentDepth;
                    while (depth <= reader.CurrentDepth)
                    {
                        reader.Read();
                    }
                    sum = stack.Pop();
                }
            }

            prevTokenType = reader.TokenType;
        }

        return sum;
    }
}
