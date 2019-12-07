using aoc.csharp._2019;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2019
{
    public class Day02Tests
    {
        private readonly ITestOutputHelper output;

        public Day02Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Answer()
        {
            using (var input = Input.Get(2019, 02))
            {
                var result = Day02.GetAnswer(input);

                Assert.Equal("4690667", result.Part1);
                Assert.Equal("6255", result.Part2);
            }
        }

        [Theory]
        [InlineData(new[] { 1, 0, 0, 0, 99 }, new[] { 2, 0, 0, 0, 99 })]
        [InlineData(new[] { 2, 3, 0, 3, 99 }, new[] { 2, 3, 0, 6, 99 })]
        [InlineData(new[] { 2, 4, 4, 5, 99, 0 }, new[] { 2, 4, 4, 5, 99, 9801 })]
        [InlineData(new[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        [InlineData(new[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 })]
        public void RunProgram(int[] memory, int[] expected)
        {
            IntcodeVm.RunProgram(memory, new Queue<int>());
            Assert.Equal(expected, memory);
        }
    }
}
