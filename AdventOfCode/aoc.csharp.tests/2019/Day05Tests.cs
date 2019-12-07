using aoc.csharp._2019;
using Xunit;

namespace aoc.csharp.tests._2019
{
    public class Day05Tests
    {
        [Fact]
        public void Answer()
        {
            using (var input = Input.Get(2019, 05))
            {
                var result = Day05.GetAnswer(input);

                Assert.Equal("16225258", result.Part1);
                Assert.Equal("2808771", result.Part2);
            }
        }
    }
}
