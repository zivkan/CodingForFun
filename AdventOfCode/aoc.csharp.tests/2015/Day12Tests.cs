using aoc.csharp._2015;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2015
{
    public class Day12Tests
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2015, 12);
            var (part1, part2) = Day12.GetAnswer(input);

            Assert.Equal("156366", part1);
            Assert.Equal("96852", part2);
        }

        [Theory]
        [InlineData("[1,2,3]", 6)]
        [InlineData("{\"a\":2,\"b\":4}", 6)]
        [InlineData("[[[3]]]", 3)]
        [InlineData("{\"a\":{\"b\":4},\"c\":-1}", 3)]
        [InlineData("{\"a\":[-1,1]}", 0)]
        [InlineData("[-1,{\"a\":1}]", 0)]
        [InlineData("[]", 0)]
        [InlineData("{}", 0)]
        public void Part1(string json, int expected)
        {
            var bytes = Encoding.UTF8.GetBytes(json);
            var actual = Day12.Part1(bytes);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[1,2,3]", 6)]
        [InlineData("[1,{\"c\":\"red\",\"b\":2},3]", 4)]
        [InlineData("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 0)]
        [InlineData("[1,\"red\",5]", 6)]
        public void Part2(string json, int expected)
        {
            var bytes = Encoding.UTF8.GetBytes(json);
            var actual = Day12.Part2(bytes);
            Assert.Equal(expected, actual);
        }
    }
}
