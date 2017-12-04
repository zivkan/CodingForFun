using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day04
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day04(ITestOutputHelper output)
        {
            _output = output;
            _input = GetPuzzleInput.DayText(4);
        }

        [Theory]
        [InlineData("aa bb cc dd ee", 1)]
        [InlineData("aa bb cc dd aa", 0)]
        [InlineData("aa bb cc dd aaa", 1)]
        public void Part1Sample(string input, int expected)
        {
            int valid = CountValidPhrases(input);
            Assert.Equal(expected, valid);
        }

        [Fact]
        public void Part1()
        {
            int validPhrases = CountValidPhrases(_input);
            _output.WriteLine("Valid = {0}", validPhrases);
        }

        [Theory]
        [InlineData("abcde fghij", 1)]
        [InlineData("abcde xyz ecdab", 0)]
        [InlineData("a ab abc abd abf abj", 1)]
        [InlineData("iiii oiii ooii oooi oooo", 1)]
        [InlineData("oiii ioii iioi iiio", 0)]
        public void Part2Sample(string input, int expected)
        {
            int valid = CountValidPhrases2(input);
            Assert.Equal(expected, valid);
        }

        [Fact]
        public void Part2()
        {
            int validPhrases = CountValidPhrases2(_input);
            _output.WriteLine("Valid = {0}", validPhrases);
        }

        private int CountValidPhrases(string input)
        {
            int count = 0;
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ISet<string> set = new HashSet<string>();
                    bool valid = true;
                    string[] split = line.Split(' ');
                    foreach (var word in split)
                    {
                        if (set.Contains(word))
                        {
                            valid = false;
                            break;
                        }
                        else
                        {
                            set.Add(word);
                        }
                    }
                    if (valid) count++;
                }
            }
            return count;
        }

        private int CountValidPhrases2(string input)
        {
            int count = 0;
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ISet<string> set = new HashSet<string>();
                    bool valid = true;
                    string[] split = line.Split(' ');
                    foreach (var word in split)
                    {
                        var orderedWord = new string(word.OrderBy(c => c).ToArray());
                        if (set.Contains(orderedWord))
                        {
                            valid = false;
                            break;
                        }
                        else
                        {
                            set.Add(orderedWord);
                        }
                    }
                    if (valid) count++;
                }
            }
            return count;
        }

    }
}
