using System.IO;

namespace aoc.csharp._2019
{
    public class Day01 : ISolver
    {
        public static int GetRequiredFuel(int mass)
        {
            var fuel = mass / 3 - 2;
            return fuel > 0 ? fuel : 0;
        }

        public static int GetTotalFuel(int mass)
        {
            int total = 0;
            int fuel = mass;
            do
            {
                fuel = GetRequiredFuel(fuel);
                total += fuel;
            }
            while (fuel > 0);

            return total;
        }

        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            int part1 = 0;
            int part2 = 0;

            string line;
            while ((line = input.ReadLine()) != null)
            {
                int mass = int.Parse(line);

                var fuel = GetRequiredFuel(mass);
                part1 += fuel;

                fuel = GetTotalFuel(mass);
                part2 += fuel;
            }

            return (part1.ToString(), part2.ToString());
        }
    }
}
