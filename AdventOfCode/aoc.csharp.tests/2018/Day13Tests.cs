﻿using aoc.csharp._2018;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2018;

public class Day13Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2018, 13);
        var (part1, part2) = Day13.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Fact]
    public void Part1Sample()
    {
        var input = @"/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   ";
        var coordinates = Day13.FirstCrashCoordinates(input);
        Assert.Equal("7,3", coordinates);
    }

    [Fact]
    public void Part2Sample()
    {
        var input = @"/>-<\  
|   |  
| /<+-\
| | | v
\>+</ |
  |   ^
  \<->/";
        var coordinates = Day13.LastCartCoordinates(input);
        Assert.Equal("6,4", coordinates);
    }
}
