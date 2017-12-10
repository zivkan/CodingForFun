using System.Collections.Generic;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day02
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day02(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(2);
        }

        [Fact]
        public void Part1Sample()
        {
            string input = "5\t1\t9\t5\n7\t5\t3\n2\t4\t6\t8";
            int actual = CalcPart1(input);
            Assert.Equal(18, actual);
        }

        [Fact]
        public void Part1()
        {
            int checksum = CalcPart1(_input);
            _output.WriteLine("Checksum = {0}", checksum);
        }

        [Fact]
        public void Part2Sample()
        {
            string input = "5\t9\t2\t8\n9\t4\t7\t3\n3\t8\t6\t5";
            int checksum = CalcPart2(input);
            Assert.Equal(9, checksum);
        }

        [Fact]
        public void Part2()
        {
            int checksum = CalcPart2(_input);
            _output.WriteLine("Checksum = {0}", checksum);
        }

        private int CalcPart1(string input)
        {
            string[] lines = input.Split('\n');
            int checksum = 0;
            foreach (string line in lines)
            {
                string[] cells = line.Split('\t');
                int min = int.Parse(cells[0]);
                int max = int.Parse(cells[0]);
                for (int i = 1; i < cells.Length; i++)
                {
                    int num = int.Parse(cells[i]);
                    if (num > max)
                    {
                        max = num;
                    }
                    if(num < min)
                    {
                        min = num;
                    }
                }

                checksum += max - min;
            }

            return checksum;
        }

        private int CalcPart2(string input)
        {
            string[] lines = input.Split('\n');
            int checksum = 0;
            foreach (string line in lines)
            {
                string[] cells = line.Split('\t');
                List<int> values = cells.Select(c => int.Parse(c)).ToList();
                int count = values.Count;
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if ((values[i] % values[j]) == 0)
                        {
                            checksum += values[i] / values[j];
                        }
                    }
                }
            }

            return checksum;
        }
    }
}
