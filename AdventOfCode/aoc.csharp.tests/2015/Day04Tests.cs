using aoc.csharp._2015;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2015;

public class Day04Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2015, 04);
        var (part1, part2) = Day04.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Theory]
    [InlineData("abcdef", 609043)]
    [InlineData("pqrstuv", 1048970)]
    public void Part1Sample(string input, int expected)
    {
        int houses = Day04.FindLowestWithFiveZeros(input);
        Assert.Equal(expected, houses);
    }
}
