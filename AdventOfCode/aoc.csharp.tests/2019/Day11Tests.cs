using aoc.csharp._2019;
using System;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day11Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2019, 11);
            var (part1, part2) = Day11.GetAnswer(input);

            Assert.Equal("2319", part1);

            var expectedPart2 =
                " #  # #### ###  ###  ###  ####  ##    ##   " + Environment.NewLine +
                " #  # #    #  # #  # #  # #    #  #    #   " + Environment.NewLine +
                " #  # ###  #  # #  # #  # ###  #       #   " + Environment.NewLine +
                " #  # #    ###  ###  ###  #    # ##    #   " + Environment.NewLine +
                " #  # #    # #  #    # #  #    #  # #  #   " + Environment.NewLine +
                "  ##  #### #  # #    #  # #     ###  ##    ";
            Assert.Equal(expectedPart2, part2);
        }
    }
}
