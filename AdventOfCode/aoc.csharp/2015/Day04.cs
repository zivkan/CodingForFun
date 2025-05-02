using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace aoc.csharp._2015
{
    public class Day04 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var key = input.ReadLine();
            var part1 = FindLowestWithFiveZeros(key).ToString();
            var part2 = FindLowestWithSixZeros(key).ToString();
            return (part1, part2);
        }

        public static int FindLowestWithFiveZeros(string key)
        {
            var mask = new byte[] { 0xFF, 0xFF, 0xF0 };
            return FindLowestMatch(key, mask);
        }

        public static int FindLowestWithSixZeros(string key)
        {
            var mask = new byte[] { 0xFF, 0xFF, 0xFF };
            return FindLowestMatch(key, mask);
        }

        public static int FindLowestMatch(string key, byte[] mask)
        {
            using (var md5 = MD5.Create())
            {
                for (int i = 0; ; i++)
                {
                    var data = key + i;
                    var input = Encoding.UTF8.GetBytes(data);
                    var hash = md5.ComputeHash(input);
                    bool match = true;
                    for (int j = 0; match && j < mask.Length; j++)
                    {
                        if ((hash[j] & mask[j]) != 0)
                        {
                            match = false;
                        }
                    }
                    if (match)
                    {
                        return i;
                    }
                }
            }
        }
    }
}
