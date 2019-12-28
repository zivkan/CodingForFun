using aoc.csharp._2018;
using Xunit;

namespace aoc.csharp.tests._2018
{
    public class Day13Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2018, 13);
            var (part1, part2) = Day13.GetAnswer(input);

            Assert.Equal("118,112", part1);
            Assert.Equal("50,21", part2);
        }

        [Fact]
        public void Part1Sample()
        {
            var input = @"/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   ";
            var coordinates = Day13.FirstCrashCoordinates(input);
            Assert.Equal("7,3", coordinates);
        }

        [Fact]
        public void Part2Sample()
        {
            var input = @"/>-<\  
|   |  
| /<+-\
| | | v
\>+</ |
  |   ^
  \<->/";
            var coordinates = Day13.LastCartCoordinates(input);
            Assert.Equal("6,4", coordinates);
        }
    }
}
