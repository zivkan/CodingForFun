using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017;

public class Day04Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2017, 04);
        var (part1, part2) = Day04.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Theory]
    [InlineData("aa bb cc dd ee", 1)]
    [InlineData("aa bb cc dd aa", 0)]
    [InlineData("aa bb cc dd aaa", 1)]
    public void Part1Sample(string input, int expected)
    {
        int valid = Day04.CountValidPhrases(input);
        Assert.Equal(expected, valid);
    }

    [Theory]
    [InlineData("abcde fghij", 1)]
    [InlineData("abcde xyz ecdab", 0)]
    [InlineData("a ab abc abd abf abj", 1)]
    [InlineData("iiii oiii ooii oooi oooo", 1)]
    [InlineData("oiii ioii iioi iiio", 0)]
    public void Part2Sample(string input, int expected)
    {
        int valid = Day04.CountValidPhrases2(input);
        Assert.Equal(expected, valid);
    }
}
