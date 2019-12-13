using aoc.csharp._2019;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day02Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2019, 02);
            var (part1, part2) = Day02.GetAnswer(input);

            Assert.Equal("4690667", part1);
            Assert.Equal("6255", part2);
        }

        [Theory]
        [InlineData(new[] { 1, 0, 0, 0, 99 }, new[] { 2, 0, 0, 0, 99 })]
        [InlineData(new[] { 2, 3, 0, 3, 99 }, new[] { 2, 3, 0, 6, 99 })]
        [InlineData(new[] { 2, 4, 4, 5, 99, 0 }, new[] { 2, 4, 4, 5, 99, 9801 })]
        [InlineData(new[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        [InlineData(new[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 })]
        public void RunProgram(int[] memory, int[] expected)
        {
            var vm = new IntcodeVm(memory);
            while (vm.Step()) ;

            int[] actual = new int[memory.Length];
            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = vm.GetMemory(i);
            }

            Assert.Equal(expected, actual);
        }
    }
}
