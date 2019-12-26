using aoc.csharp._2016;
using System.Linq;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day10Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2016, 10);
            var (part1, part2) = Day10.GetAnswer(input);

            Assert.Equal("161", part1);
            Assert.Equal("133163", part2);
        }

        [Fact]
        public void Sample()
        {
            string input = "value 5 goes to bot 2\r\n" +
                "bot 2 gives low to bot 1 and high to bot 0\r\n" +
                "value 3 goes to bot 1\r\n" +
                "bot 1 gives low to output 1 and high to bot 0\r\n" +
                "bot 0 gives low to output 2 and high to output 0\r\n" +
                "value 2 goes to bot 2";

            var specification = Day10.ParseSpecification(input);
            var botsList = Day10.ConfigureBots(specification.Item1, specification.Item2);

            var outputBins = Day10.GetOutputBinTransfers(botsList);

            Assert.True(outputBins.ContainsKey(0));
            Assert.Equal(5, outputBins[0]);
            Assert.True(outputBins.ContainsKey(1));
            Assert.Equal(2, outputBins[1]);
            Assert.True(outputBins.ContainsKey(2));
            Assert.Equal(3, outputBins[2]);

            var bot2 = botsList.Single(b => b.Number == 2);
            Assert.True(bot2.TransferDetails.Any(td => td.MicrochipValue == 5) && bot2.TransferDetails.Any(td => td.MicrochipValue == 2));
        }
    }
}
