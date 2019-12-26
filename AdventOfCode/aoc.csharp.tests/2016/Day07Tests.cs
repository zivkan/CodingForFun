﻿using aoc.csharp._2016;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day07Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2016, 07);
            var (part1, part2) = Day07.GetAnswer(input);

            Assert.Equal("118", part1);
            Assert.Equal("260", part2);
        }

        [Theory]
        [InlineData("abba[mnop]qrst", true)]
        [InlineData("abcd[bddb]xyyx", false)]
        [InlineData("aaaa[qwer]tyui", false)]
        [InlineData("ioxxoj[asdfgh]zxcvbn", true)]
        public void Part1Samples(string input, bool expected)
        {
            bool actual = Day07.SupportsTls(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("aba[bab]xyz", true)]
        [InlineData("xyx[xyx]xyx", false)]
        [InlineData("aaa[kek]eke", true)]
        [InlineData("zazbz[bzb]cdb", true)]
        public void Part2Samples(string input, bool expected)
        {
            bool actual = Day07.SupportsSsl(input);
            Assert.Equal(expected, actual);
        }
    }
}
