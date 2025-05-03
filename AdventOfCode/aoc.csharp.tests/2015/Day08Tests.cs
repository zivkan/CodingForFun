using aoc.csharp._2015;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2015;

public class Day08Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2015, 08);
        var (part1, part2) = Day08.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Theory]
    [InlineData(@"""""", "")]
    [InlineData(@"""abc""", "abc")]
    [InlineData(@"""aaa\""aaa""", "aaa\"aaa")]
    [InlineData(@"""\x27""", "'")]
    public void Part1Test(string input, string expected)
    {
        var actual = Day08.ParseLine(input);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("\"\"", "\"\\\"\\\"\"")]
    [InlineData("\"abc\"", "\"\\\"abc\\\"\"")]
    [InlineData("\"aaa\\\"aaa\"", "\"\\\"aaa\\\\\\\"aaa\\\"\"")]
    [InlineData("\"\\x27\"", "\"\\\"\\\\x27\\\"\"")]
    public void Part2Test(string input, string expected)
    {
        var actual = Day08.EncodeLine(input);
        Assert.Equal(expected, actual);
    }
}
