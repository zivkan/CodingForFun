using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2018
{
    public class Day05 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var part1 = React(text);
            var part2 = BestReaction(text);
            return (part1.Length.ToString(), part2.Length.ToString());
        }

        public static string React(string input)
        {
            var reactions = new List<string>();
            var polymers = new char[2];
            for (char polymer = 'a'; polymer <= 'z'; polymer++)
            {
                polymers[0] = polymer;
                polymers[1] = char.ToUpper(polymer);
                reactions.Add(new string(polymers));
                polymers[0] = char.ToUpper(polymer);
                polymers[1] = polymer;
                reactions.Add(new string(polymers));
            }

            string result = input;
            int before;
            do
            {
                before = result.Length;
                foreach (var reaction in reactions)
                {
                    result = result.Replace(reaction, string.Empty);
                }
            } while (before != result.Length);

            return result;
        }

        public static string BestReaction(string input)
        {
            string best = input;
            char[] chars = new char[1];

            for (char c = 'a'; c <= 'z'; c++)
            {
                chars[0] = c;
                string test = input.Replace(new string(chars), string.Empty);
                chars[0] = char.ToUpperInvariant(c);
                test = test.Replace(new string(chars), string.Empty);

                var result = React(test);
                if (result.Length < best.Length)
                {
                    best = result;
                }
            }

            return best;
        }
    }
}
