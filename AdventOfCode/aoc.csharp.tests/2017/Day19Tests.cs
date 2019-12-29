using aoc.csharp._2017;
using Xunit;

namespace aoc.csharp.tests._2017
{
    public class Day19Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2017, 19);
            var (part1, part2) = Day19.GetAnswer(input);

            Assert.Equal("GINOWKYXH", part1);
            Assert.Equal("16636", part2);
        }

        [Fact]
        public void Sample()
        {
            const string input = @"     |          
     |  +--+    
     A  |  C    
 F---|----E|--+ 
     |  |  |  D 
     +B-+  +--+ 
                ";

            var (path, steps) = Day19.FindPath(input);
            Assert.Equal("ABCDEF", path);
            Assert.Equal(38, steps);
        }
    }
}
