using aoc.csharp._2018;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2018;

public class Day08Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2018, 08);
        var (part1, part2) = Day08.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    private static readonly string _sampleInput =
        "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

    [Fact]
    public void Part1Sample()
    {
        var result = Day08.GetSumOfMetadata(_sampleInput);
        Assert.Equal(138, result);
    }

    [Fact]
    public void Part2Sample()
    {
        var result = Day08.GetRootNodeValue(_sampleInput);
        Assert.Equal(66, result);
    }
}
