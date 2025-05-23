﻿using aoc.csharp._2019;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2019;

public class Day13Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2019, 13);
        var (part1, part2) = Day13.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }
}
