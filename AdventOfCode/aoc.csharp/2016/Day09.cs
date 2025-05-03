using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2016;

public class Day09 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        string text = input.ReadLine();
        int part1 = CalculateLength(text);
        long part2 = CalculateLengthRecursive(text);

        return (part1.ToString(), part2.ToString());
    }

    public static int CalculateLength(string input)
    {
        var parts = ParseInput(input);

        int length = 0;
        foreach (var part in parts)
        {
            length += part switch
            {
                Literal literal => literal.Value.Length,
                Marker marker => marker.Count * marker.InnerValue.Length,
                _ => throw new Exception("Unsupported part type"),
            };
        }

        return length;
    }

    public static long CalculateLengthRecursive(string input)
    {
        var parts = ParseInput(input);

        long length = 0;
        foreach (var part in parts)
        {
            length += part switch
            {
                Literal literal => literal.Value.Length,
                Marker marker => marker.Count * CalculateLengthRecursive(marker.InnerValue),
                _ => throw new Exception("Unsupported part type"),
            };
        }

        return length;
    }

    private static List<IPart> ParseInput(string input)
    {
        List<IPart> parts = new List<IPart>();

        int currentPosition = 0;
        int markerIndex;
        while ((markerIndex = input.IndexOf("(", currentPosition, StringComparison.Ordinal)) != -1)
        {
            if (markerIndex != currentPosition)
            {
                parts.Add(new Literal(input[currentPosition..markerIndex]));
            }

            int commaIndex = input.IndexOf("x", markerIndex + 1, StringComparison.Ordinal);
            int closeIndex = input.IndexOf(")", commaIndex + 1, StringComparison.Ordinal);

            if (commaIndex == -1 || closeIndex == -1)
            {
                throw new Exception("invalid input");
            }

            int characterCount = int.Parse(input.Substring(markerIndex + 1, commaIndex - markerIndex - 1));
            int repeatCount = int.Parse(input.Substring(commaIndex + 1, closeIndex - commaIndex - 1));
            parts.Add(new Marker(repeatCount, input.Substring(closeIndex + 1, characterCount)));
            currentPosition = closeIndex + characterCount + 1;
        }

        if (currentPosition < input.Length)
        {
            parts.Add(new Literal(input.Substring(currentPosition)));
        }

        return parts;
    }

    private interface IPart
    {
    }

    private class Literal : IPart
    {
        public string Value { get; }

        public Literal(string value)
        {
            Value = value;
        }
    }

    private class Marker : IPart
    {
        public int Count { get; }
        public string InnerValue { get; }

        public Marker(int count, string innerValue)
        {
            Count = count;
            InnerValue = innerValue;
        }
    }
}
