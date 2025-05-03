using aoc.csharp._2015;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2015;

public class Day01Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2015, 01);
        var (part1, part2) = Day01.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Theory]
    [InlineData("(())", 0)]
    [InlineData("()()", 0)]
    [InlineData("(((", 3)]
    [InlineData("(()(()(", 3)]
    [InlineData("))(((((", 3)]
    [InlineData("())", -1)]
    [InlineData("))(", -1)]
    [InlineData(")))", -3)]
    [InlineData(")())())", -3)]
    public void Part1Sample(string input, int expected)
    {
        string actual;
        using (var reader = new StringReader(input))
        {
            (actual, _) = Day01.GetAnswer(reader);
        }
        Assert.Equal(expected.ToString(), actual);
    }

    [Theory]
    [InlineData(")", 1)]
    [InlineData("()())", 5)]
    public void Part2Sample(string input, int expected)
    {
        string actual;
        using (var reader = new StringReader(input))
        {
            (_, actual) = Day01.GetAnswer(reader);
        }
        Assert.Equal(expected.ToString(), actual);
    }
}
