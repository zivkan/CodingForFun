using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2022
{
    public class Day01 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var elves = Parse(input);
            var elfCalories = GetCalorieTotal(elves);

            string part1 = elfCalories.Max().ToString();
            string part2 = elfCalories.OrderByDescending(c => c).Take(3).Sum().ToString();

            return (part1, part2);
        }

        public static IReadOnlyList<IReadOnlyList<int>> Parse(TextReader input)
        {
            var elves = new List<IReadOnlyList<int>>();
            var currentElf = new List<int>();

            string? line;
            while ((line = input.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    int calories = int.Parse(line);
                    currentElf.Add(calories);
                }
                else
                {
                    elves.Add(currentElf);
                    currentElf = new List<int>();
                }
            }

            if (currentElf.Count > 0)
            {
                elves.Add(currentElf);
            }

            return elves;
        }

        public static IReadOnlyList<int> GetCalorieTotal(IReadOnlyList<IReadOnlyList<int>> elves)
        {
            var elfTotals = new List<int>(elves.Count);

            for (int elf = 0; elf <  elves.Count; elf++)
            {
                var total = elves[elf].Sum();
                elfTotals.Add(total);
            }

            return elfTotals;
        }
    }
}
