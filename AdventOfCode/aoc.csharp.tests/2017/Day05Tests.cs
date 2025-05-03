using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017;

public class Day05Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2017, 05);
        var (part1, part2) = Day05.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    private const string SampleInput = "0\n3\n0\n1\n-3";

    [Theory]
    [InlineData(SampleInput, 5, new[] {2, 5, 0, 1, -2})]
    public void Part1Sample(string input, int expectedSteps, int[] expectedOffsets)
    {
        var (steps, offsets) = Day05.CountSteps(input, Day05.Part1Increment);
        Assert.Equal(expectedSteps, steps);
        Assert.Equal(expectedOffsets, offsets);
    }

    [Theory]
    [InlineData(SampleInput, 10, new[] { 2, 3, 2, 3, -1 })]
    public void Part2Sample(string input, int expectedSteps, int[] expectedOffsets)
    {
        var (steps, offsets) = Day05.CountSteps(input, Day05.Part2Increment);
        Assert.Equal(expectedSteps, steps);
        Assert.Equal(expectedOffsets, offsets);
    }
}
