using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2017
{
    public class Day02 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            int part1 = CalcPart1(text);
            int part2 = CalcPart2(text);
            return (part1.ToString(), part2.ToString());
        }

        public static int CalcPart1(string input)
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
                    if (num < min)
                    {
                        min = num;
                    }
                }

                checksum += max - min;
            }

            return checksum;
        }

        public static int CalcPart2(string input)
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
