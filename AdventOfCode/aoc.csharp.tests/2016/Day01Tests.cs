﻿using aoc.csharp._2016;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day01Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2016, 01);
            var (part1, part2) = Day01.GetAnswer(input);

            Assert.Equal("209", part1);
            Assert.Equal("136", part2);
        }

        [Theory]
        [InlineData("R2, L3", 5)]
        [InlineData("R2, R2, R2", 2)]
        [InlineData("R5, L5, R5, R3", 12)]
        public void Part1Sample(string input, uint expected)
        {
            var actual = Day01.GetDistanceFromOrigin(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("R8, R4, R4, R8", 4)]
        public void Part2Sample(string input, uint expected)
        {
            var distance = Day01.GetDistanceFromOriginOfFirstRepeatedLocation(input);

            Assert.Equal(expected, distance);
        }
    }
}
