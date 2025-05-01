using aoc.csharp._2019;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day02Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2019, 02);
            var (part1, part2) = Day02.GetAnswer(input);

            Assert.Equal("4690667", part1);
            Assert.Equal("6255", part2);
        }

        [Theory]
        [InlineData(new long[] { 1, 0, 0, 0, 99 }, new long[] { 2, 0, 0, 0, 99 })]
        [InlineData(new long[] { 2, 3, 0, 3, 99 }, new long[] { 2, 3, 0, 6, 99 })]
        [InlineData(new long[] { 2, 4, 4, 5, 99, 0 }, new long[] { 2, 4, 4, 5, 99, 9801 })]
        [InlineData(new long[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new long[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        [InlineData(new long[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new long[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 })]
        public void RunProgram(long[] memory, long[] expected)
        {
            var vm = new IntcodeVm(memory);
            while (vm.Step()) ;

            var actual = new long[memory.Length];
            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = vm.GetMemory(i);
            }

            Assert.Equal(expected, actual);
        }
    }
}
