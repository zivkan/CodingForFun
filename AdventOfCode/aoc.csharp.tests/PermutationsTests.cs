using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace aoc.csharp.tests
{
    public class PermutationsTests
    {
        [Fact]
        public void Generate1()
        {
            var expected = new List<int[]>()
            {
                new [] { 0 }
            };

            var actual = Permutations.Generate(expected[0]).ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Generate2()
        {
            var expected = new List<string[]>()
            {
                new [] { "one", "two" },
                new [] { "two", "one" }
            };

            var actual = Permutations.Generate(expected[0]).ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Generate3()
        {
            var expected = new List<int[]>()
            {
                new [] { 0, 1, 2 },
                new [] { 0, 2, 1 },
                new [] { 1, 0, 2 },
                new [] { 1, 2, 0 },
                new [] { 2, 0, 1 },
                new [] { 2, 1, 0 }
            };

            var actual = Permutations.Generate(expected[0]).ToList();

            Assert.Equal(expected, actual);
        }
    }
}
