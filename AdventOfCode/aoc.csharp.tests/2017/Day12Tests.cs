using aoc.csharp._2017;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017;

public class Day12Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2017, 12);
        var (part1, part2) = Day12.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    private const string _sample = @"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5";

    [Fact]
    public void Sample()
    {
        var groups = Day12.GroupPrograms(_sample);
        Assert.Equal(6, groups.Single(g=>g.Contains(0)).Count);
        Assert.Equal(2, groups.Count);
    }
}
