using aoc.csharp._2019;
using System;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day11Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2019, 11);
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
