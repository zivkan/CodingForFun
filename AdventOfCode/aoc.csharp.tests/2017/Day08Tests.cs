using aoc.csharp._2017;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017;

public class Day08Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2017, 08);
        var (part1, part2) = Day08.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    private const string _sample = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";

    [Fact]
    public void SampleInput()
    {
        var (registers,maxValue) = Day08.RunProgram(_sample);
        var highestRegister = registers.Values.Max();
        Assert.Equal(1, highestRegister);
        Assert.Equal(10, maxValue);
    }
}
