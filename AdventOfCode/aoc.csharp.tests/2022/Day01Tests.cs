using aoc.csharp._2022;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace aoc.csharp.tests._2022
{
    public class Day01Tests
    {
        private const string Sample = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";

        [Fact]
        public async Task Answer()
        {
            var input = await Input.GetAsync(2022, 1);
            var (part1, part2) = Day01.GetAnswer(input);

            Assert.Equal("69501", part1);
            Assert.Equal("202346", part2);
        }

        [Fact]
        public void Parse()
        {
            var elves = Day01.Parse(new StringReader(Sample));

            Assert.Equal(5, elves.Count);
            Assert.Equal(new[] { 1000, 2000, 3000 }, elves[0]);
            Assert.Equal(new[] { 4000 }, elves[1]);
            Assert.Equal(new[] { 5000, 6000 }, elves[2]);
            Assert.Equal(new[] { 7000, 8000, 9000 }, elves[3]);
            Assert.Equal(new[] { 10000 }, elves[4]);
        }

        [Fact]
        public void GetCalorieTotals()
        {
            var elves = Day01.Parse(new StringReader(Sample));
            var elfCalories = Day01.GetCalorieTotal(elves);

            Assert.Equal(5, elfCalories.Count);
            Assert.Equal(6000, elfCalories[0]);
            Assert.Equal(4000, elfCalories[1]);
            Assert.Equal(11000, elfCalories[2]);
            Assert.Equal(24000, elfCalories[3]);
            Assert.Equal(10000, elfCalories[4]);
        }
    }
}
