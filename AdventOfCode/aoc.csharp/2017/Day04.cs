using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2017;

public class Day04 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var text = input.ReadToEnd();
        int part1 = CountValidPhrases(text);
        int part2 = CountValidPhrases2(text);
        return (part1.ToString(), part2.ToString());
    }

    public static int CountValidPhrases(string input)
    {
        int count = 0;
        using (var reader = new StringReader(input))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                ISet<string> set = new HashSet<string>();
                bool valid = true;
                string[] split = line.Split(' ');
                foreach (var word in split)
                {
                    if (set.Contains(word))
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        set.Add(word);
                    }
                }
                if (valid) count++;
            }
        }
        return count;
    }

    public static int CountValidPhrases2(string input)
    {
        int count = 0;
        using (var reader = new StringReader(input))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                ISet<string> set = new HashSet<string>();
                bool valid = true;
                string[] split = line.Split(' ');
                foreach (var word in split)
                {
                    var orderedWord = new string(word.OrderBy(c => c).ToArray());
                    if (set.Contains(orderedWord))
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        set.Add(orderedWord);
                    }
                }
                if (valid) count++;
            }
        }
        return count;
    }
}
