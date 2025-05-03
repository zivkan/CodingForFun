using aoc.csharp._2016;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2016;

public class Day01Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2016, 01);
        var (part1, part2) = Day01.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Theory]
    [InlineData("R2, L3", 5)]
    [InlineData("R2, R2, R2", 2)]
    [InlineData("R5, L5, R5, R3", 12)]
    public void Part1Sample(string input, uint expected)
    {
        var actual = Day01.GetDistanceFromOrigin(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("R8, R4, R4, R8", 4)]
    public void Part2Sample(string input, uint expected)
    {
        var distance = Day01.GetDistanceFromOriginOfFirstRepeatedLocation(input);

        Assert.Equal(expected, distance);
    }
}
