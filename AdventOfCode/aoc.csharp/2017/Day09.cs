using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2017
{
    public class Day09 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var (score, chars) = GetScore(text);
            return (score.ToString(), chars.ToString());
        }

        public static (int score, int chars) GetScore(string input)
        {
            List<int> groups = new List<int>();
            int depth = 0;
            int chars = 0;
            bool inGarbage = false;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '{' && !inGarbage)
                {
                    depth++;
                    groups.Add(depth);
                }
                else if (c == '}' && !inGarbage)
                {
                    depth--;
                }
                else if (c == '<' && !inGarbage)
                {
                    inGarbage = true;
                }
                else if (c == '>')
                {
                    inGarbage = false;
                }
                else if (c == '!')
                {
                    i++;
                }
                else if (inGarbage)
                {
                    chars++;
                }
            }

            return (groups.Sum(), chars);
        }
    }
}
