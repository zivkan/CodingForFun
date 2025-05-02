using System.Collections.Generic;
using System.IO;
using System.Text;

namespace aoc.csharp._2015
{
    public class Day10 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var start = input.ReadLine();

            string part1, part2;

            using (var enumerator = Game(start).GetEnumerator())
            {
                for (int i = 0; i < 40; i++)
                {
                    enumerator.MoveNext();
                }
                part1 = enumerator.Current;

                for (int i = 0; i < 10; i++)
                {
                    enumerator.MoveNext();
                }
                part2 = enumerator.Current;
            }

            return (part1.Length.ToString(), part2.Length.ToString());
        }

        public static IEnumerable<string> Game(string start)
        {
            StringBuilder sb = new StringBuilder();
            string last = start;

            static void Append(StringBuilder sb, char c, int count)
            {
                sb.Append(count);
                sb.Append(c);
            }

            for (; ; )
            {
                sb.Clear();
                char c = last[0];
                int count = 1;

                for (int i = 1; i < last.Length; i++)
                {
                    char next = last[i];
                    if (next != c)
                    {
                        Append(sb, c, count);
                        c = next;
                        count = 1;
                    }
                    else
                    {
                        count++;
                    }
                }

                Append(sb, c, count);
                last = sb.ToString();
                yield return last;
            }
        }
    }
}
