﻿using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017;

public class Day11Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2017, 11);
        var (part1, part2) = Day11.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Theory]
    [InlineData("ne,ne,ne", 3)]
    [InlineData("ne,ne,sw,sw", 0)]
    [InlineData("ne,ne,s,s", 2)]
    [InlineData("se,sw,se,sw,sw", 3)]
    public void Sample(string input, int expected)
    {
        var (last, _) = Day11.GetDistances(input);
        Assert.Equal(expected, last);
    }
}
