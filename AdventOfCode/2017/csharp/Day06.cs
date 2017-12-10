using System;
using System.Collections.Generic;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day06
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day06(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(6);
        }

        [Fact]
        public void Part1Sample()
        {
            const string input = "0\t2\t7\t0";
            var (cycles, loopLength, banks) = IterateUntilRepeat(input);
            Assert.Equal(5, cycles);
            Assert.Equal(banks, new[] { 2, 4, 1, 2 });
            Assert.Equal(4, loopLength);
        }

        [Fact]
        public void Part1()
        {
            var (cycles, looplength, banks) = IterateUntilRepeat(_input);
            _output.WriteLine("Cycles = {0}", cycles);
        }

        [Fact] void Part2()
        {
            var (cycles, looplength, banks) = IterateUntilRepeat(_input);
            _output.WriteLine("Loop length = {0}", looplength);
        }

        private (int cycles, int loopLength, int[] banks) IterateUntilRepeat(string input)
        {
            int cycles = 0;
            var banks = input.Split('\t').Select(int.Parse).ToArray();
            int count = banks.Length;

            List<int[]> history = new List<int[]>();
            history.Add(banks);

            while (true && cycles < 1000000)
            {
                cycles++;
                int max = banks[0];
                int chosenBank = 0;
                for (int i = 0; i < count; i++)
                {
                    if (banks[i] > max)
                    {
                        max = banks[i];
                        chosenBank = i;
                    }
                }

                int[] newBanks = (int[])banks.Clone();
                newBanks[chosenBank] = 0;
                for (int i = 0; i < max; i++)
                {
                    chosenBank = (chosenBank + 1) % count;
                    newBanks[chosenBank]++;
                }

                for (var j=0; j < history.Count; j++)
                {
                    var oldBank = history[j];
                    bool same = true;
                    for (int i = 0; i < count; i++)
                    {
                        if (oldBank[i] != newBanks[i])
                        {
                            same = false;
                            break;
                        }
                    }

                    if (same)
                    {
                        int loopLength = history.Count - j;
                        return (cycles, loopLength, newBanks);
                    }
                }

                history.Add(newBanks);
                banks = newBanks;
            }

            throw new Exception();
        }
    }
}
