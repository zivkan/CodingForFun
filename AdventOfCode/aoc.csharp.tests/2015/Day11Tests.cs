using aoc.csharp._2015;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2015;

public class Day11Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2015, 11);
        var (part1, part2) = Day11.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Theory]
    [InlineData("hijklmmn", false)]
    [InlineData("abbceffg", false)]
    [InlineData("abbcegjk", false)]
    [InlineData("abcdffaa", true)]
    [InlineData("ghjaabcc", true)]
    public void IsValidTests(string input, bool expected)
    {
        var actual = Day11.IsValid(input);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcdefgh", "abcdffaa")]
    [InlineData("ghijklmn", "ghjaabcc")]
    public void NextTests(string input, string expected)
    {
        var actual = Day11.Next(input);
        Assert.Equal(expected, actual);
    }
}
