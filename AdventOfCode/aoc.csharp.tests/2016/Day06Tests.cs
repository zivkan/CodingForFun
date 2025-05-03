using aoc.csharp._2016;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2016;

public class Day06Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2016, 06);
        var (part1, part2) = Day06.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    private static readonly IReadOnlyList<string> SampleInput = new List<string>
    {
        "eedadn",
        "drvtee",
        "eandsr",
        "raavrd",
        "atevrs",
        "tsrnev",
        "sdttsa",
        "rasrtv",
        "nssdts",
        "ntnada",
        "svetve",
        "tesnvt",
        "vntsnd",
        "vrdear",
        "dvrsen",
        "enarar"
    };

    [Fact]
    public void Part1Sample()
    {
        var code = Day06.GetMostRepeatedCode(SampleInput);
        Assert.Equal("easter", code);
    }

    [Fact]
    public void Part2Sample()
    {
        var code = Day06.GetLeastRepeatedCode(SampleInput);
        Assert.Equal("advent", code);
    }
}
