using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017;

public class Day15Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2017, 15);
        var (part1, part2) = Day15.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Fact]
    public void Sample()
    {
        var matches = Day15.CountMatches(Day15.Generator(65, Day15.GeneratorAMultiplier, 1),
            Day15.Generator(8921, Day15.GeneratorBMultiplier, 1),
            40_000_000);
        Assert.Equal(588, matches);

        matches = Day15.CountMatches(Day15.Generator(65, Day15.GeneratorAMultiplier, 4),
            Day15.Generator(8921, Day15.GeneratorBMultiplier, 8),
            5_000_000);
        Assert.Equal(309, matches);
    }
}
