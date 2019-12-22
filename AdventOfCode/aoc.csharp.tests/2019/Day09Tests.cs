﻿using aoc.csharp._2019;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day09Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2019, 09);
            var (part1, part2) = Day09.GetAnswer(input);

            Assert.Equal("3518157894", part1);
            Assert.Equal("80379", part2);
        }

        [Theory]
        [InlineData(new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 }, new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 })]
        [InlineData(new long[] { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 }, new long[] { 1219070632396864 })]
        [InlineData(new long[] { 104, 1125899906842624, 99 }, new long[] { 1125899906842624 })]
        public void SampleVmInput(long[] program, long[] expected)
        {
            var vm = new IntcodeVm(program);
            while (vm.Step()) ;

            var output = new long[vm.Output.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = vm.Output.Dequeue();
            }
            Assert.Equal(expected, output);
        }
    }
}
