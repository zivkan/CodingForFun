using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day9
    {
        private readonly ITestOutputHelper _output;

        public Day9(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData("ADVENT", 6)]
        [InlineData("A(1x5)BC", 7)]
        [InlineData("(3x3)XYZ", 9)]
        [InlineData("A(2x2)BCD(2x2)EFG", 11)]
        [InlineData("(6x1)(1x3)A", 6)]
        [InlineData("X(8x2)(3x3)ABCY", 18)]
        public void Part1Samples(string input, int expected)
        {
            int actual = CalculateLength(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Part1()
        {
            string input = GetPuzzleInput.DayText(9);
            int length = CalculateLength(input);
            _output.WriteLine("{0}", length);
        }

        [Theory]
        [InlineData("(3x3)XYZ", 9)]
        [InlineData("X(8x2)(3x3)ABCY", 20)]
        [InlineData("(27x12)(20x12)(13x14)(7x10)(1x12)A", 241920)]
        [InlineData("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", 445)]
        public void Part2Samples(string input, int expected)
        {
            long actual = CalculateLengthRecursive(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Part2()
        {
            string input = GetPuzzleInput.DayText(9);
            long length = CalculateLengthRecursive(input);
            _output.WriteLine("{0}", length);
        }

        private int CalculateLength(string input)
        {
            var parts = ParseInput(input);

            int length = 0;
            foreach (var part in parts)
            {
                Literal literal = part as Literal;
                if (literal != null)
                {
                    length += literal.Value.Length;
                }
                else
                {
                    Marker marker = part as Marker;
                    if (marker != null)
                    {
                        length += marker.Count*marker.InnerValue.Length;
                    }
                    else
                    {
                        throw new Exception("Unsupported part type");
                    }
                }
            }

            return length;
        }

        private long CalculateLengthRecursive(string input)
        {
            var parts = ParseInput(input);

            long length = 0;
            foreach (var part in parts)
            {
                Literal literal = part as Literal;
                if (literal != null)
                {
                    length += literal.Value.Length;
                }
                else
                {
                    Marker marker = part as Marker;
                    if (marker != null)
                    {
                        length += marker.Count * CalculateLengthRecursive(marker.InnerValue);
                    }
                    else
                    {
                        throw new Exception("Unsupported part type");
                    }
                }
            }

            return length;
        }

        private List<IPart> ParseInput(string input)
        {
            List<IPart> parts = new List<IPart>();

            int currentPosition = 0;
            int markerIndex;
            while ((markerIndex = input.IndexOf("(", currentPosition, StringComparison.Ordinal)) != -1)
            {
                if (markerIndex != currentPosition)
                {
                    parts.Add(new Literal(input.Substring(currentPosition, markerIndex-currentPosition)));
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
}
