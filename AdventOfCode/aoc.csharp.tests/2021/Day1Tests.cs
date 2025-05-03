using aoc.csharp._2021;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2021;

public class Day01Tests(ITestOutputHelper _output)
{
    private readonly IReadOnlyList<int> Sample = new int[]
    {
            199, 200, 208, 210, 200, 207, 240, 269, 260, 263
    };

    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2021, 01);
        var (part1, part2) = Day01.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Fact]
    public void Part1()
    {
        var actual = Day01.Part1(Sample);
        int expected = 7;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Part2()
    {
        var actual = Day01.Part2(Sample);
        int expected = 5;
        Assert.Equal(expected, actual);
    }
}
