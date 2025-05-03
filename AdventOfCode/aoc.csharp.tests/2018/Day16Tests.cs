using aoc.csharp._2018;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2018;

public class Day16Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2018, 16);
        var (part1, part2) = Day16.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Fact]
    public void Samples()
    {
        var input = new Day16.Sample(
            new Day16.Instruction(9, 2, 1, 2),
            new List<int> { 3, 2, 1, 1 },
            new List<int> { 3, 2, 2, 1 }
            );
        var result = Day16.Possibilities(input);

        Assert.Equal(3, result.Count);
        Assert.Contains("mulr", result);
        Assert.Contains("addi", result);
        Assert.Contains("seti", result);
    }
}
