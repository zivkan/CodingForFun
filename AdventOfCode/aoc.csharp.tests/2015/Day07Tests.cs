using aoc.csharp._2015;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2015;

public class Day07Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2015, 07);
        var (part1, part2) = Day07.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
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
