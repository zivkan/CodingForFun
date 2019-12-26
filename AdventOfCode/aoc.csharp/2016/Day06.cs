using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2016
{
    public class Day06 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var messages = Input.GetLines(input);
            var part1 = GetMostRepeatedCode(messages);
            var part2 = GetLeastRepeatedCode(messages);
            return (part1, part2);
        }

        public static string GetMostRepeatedCode(IReadOnlyList<string> messages)
        {
            char[] code = new char[messages[0].Length];

            for (int i = 0; i < messages[0].Length; i++)
            {
                var charCount = GetCharCount(messages, i);

                code[i] = charCount.OrderByDescending(cc => cc.Value).Select(cc => cc.Key).First();
            }

            return new string(code);
        }

        public static string GetLeastRepeatedCode(IReadOnlyList<string> messages)
        {
            char[] code = new char[messages[0].Length];

            for (int i = 0; i < messages[0].Length; i++)
            {
                var charCount = GetCharCount(messages, i);

                code[i] = charCount.OrderBy(cc => cc.Value).Select(cc => cc.Key).First();
            }

            return new string(code);
        }

        private static Dictionary<char, int> GetCharCount(IReadOnlyList<string> messages, int position)
        {
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            foreach (string message in messages)
            {
                char c = message[position];
                if (!charCount.TryGetValue(c, out int count))
                {
                    count = 0;
                }
                count++;
                charCount[c] = count;
            }
            return charCount;
        }
    }
}
