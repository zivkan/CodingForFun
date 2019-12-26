using System.IO;
using System.Linq;

namespace aoc.csharp._2015
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
            var part1 = CalculateRequiredArea(text);
            var part2 = CalculateRibbon(text);
            return (part1.ToString(), part2.ToString());
        }

        public static int CalculateRequiredArea(string input)
        {
            int total = 0;
            using (var reader = new StringReader(input))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split('x');
                    var dimensions = split.Select(int.Parse).ToList();
                    int smallestSide = dimensions.OrderBy(i => i).Take(2).Aggregate(1, (acc, i) => acc * i);
                    total += 2 * dimensions[0] * dimensions[1] +
                        2 * dimensions[0] * dimensions[2] +
                        2 * dimensions[1] * dimensions[2] +
                        smallestSide;
                }
            }

            return total;
        }

        public static int CalculateRibbon(string input)
        {
            int total = 0;
            using (var reader = new StringReader(input))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split('x');
                    var dimensions = split.Select(int.Parse).ToList();
                    int wrapping = dimensions.OrderBy(i => i).Take(2).Aggregate(0, (acc, i) => acc + 2 * i);
                    int bow = dimensions[0] * dimensions[1] * dimensions[2];
                    total += wrapping + bow;
                }
            }

            return total;
        }
    }
}
