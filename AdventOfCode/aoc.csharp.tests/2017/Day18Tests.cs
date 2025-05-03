using aoc.csharp._2017;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2017
{
    public class Day18Tests(ITestOutputHelper _output)
    {
        [Fact]
        public async Task Answer()
        {
            using var input = await Input.GetAsync(2017, 18);
            var (part1, part2) = Day18.GetAnswer(input);

            _output.WriteLine($"Part 1: {part1}");
            _output.WriteLine($"Part 2: {part2}");
        }

        [Fact]
        public void Sample()
        {
            const string part1Input = @"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";
            var programOutput = Day18.Part1(part1Input);

            Assert.NotEmpty(programOutput);
            Assert.Equal(4, programOutput[programOutput.Count - 1]);

            const string part2Input = @"snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d";

            programOutput = Day18.Part2(part2Input);
            Assert.Equal(3, programOutput.Count);
            Assert.Equal(1, programOutput[0]);
            Assert.Equal(2, programOutput[1]);
            Assert.Equal(1, programOutput[2]);
        }
    }
}
