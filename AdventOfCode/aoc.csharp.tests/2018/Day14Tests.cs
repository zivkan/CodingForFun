using aoc.csharp._2018;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2018;

public class Day14Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2018, 14);
        var (part1, part2) = Day14.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Theory]
    [InlineData(9, "5158916779")]
    [InlineData(5, "0124515891")]
    [InlineData(18, "9251071085")]
    [InlineData(2018, "5941429882")]
    public void Part1Sample(int iterations, string expected)
    {
        var score = Day14.GetScore(iterations);
        Assert.Equal(expected, score);
    }

    [Theory]
    [InlineData("51589", 9)]
    [InlineData("01245", 5)]
    [InlineData("92510", 18)]
    [InlineData("59414", 2018)]
    public void Part2Sample(string input, int expected)
    {
        var iterations = Day14.GetIterations(input);
        Assert.Equal(expected, iterations);
    }
}
