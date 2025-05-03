using aoc.csharp._2019;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2019
{
    public class Day08Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2019, 08);
            var (part1, part2) = Day08.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2:\n{part2}");
        }

        [Fact]
        public void FlattenLayersTest()
        {
            using var input = new StringReader("0222112222120000");
            var layers = Day08.GetLayers(2, 2, input);
            var image = Day08.FlattenLayers(layers);
            Assert.Equal(new[] { '0', '1', '1', '0' }, image);
        }
    }
}
