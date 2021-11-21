using aoc.csharp._2015;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day07Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2015, 07);
            var (part1, part2) = Day07.GetAnswer(input);

            Assert.Equal("956", part1);
            Assert.Equal("40149", part2);
        }

        [Fact]
        public void Part1Test()
        {
            var input = @"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i";
            using (var reader = new StringReader(input))
            {
                List<Day07.Gate>? gates = Day07.ParseInput(reader);
                Dictionary<string, ushort> wires = Day07.GetFinal(gates);

                Dictionary<string, ushort> expected = new()
                {
                    ["d"] = 72,
                    ["e"] = 507,
                    ["f"] = 492,
                    ["g"] = 114,
                    ["h"] = 65412,
                    ["i"] = 65079,
                    ["x"] = 123,
                    ["y"] = 456
                };

                Assert.Equal(expected, wires);
            }
        }
    }
}
