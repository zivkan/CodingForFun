using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2017
{
    public class Day10 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var list = GetResult(256, text);
            int part1 = list[0] * list[1];
            string part2 = KnotHash(text);
            return (part1.ToString(), part2);
        }

        public static byte[] KnotHashBytes(string input)
        {
            List<int> instructions = new List<int>(input.Length + 5);
            for (int i = 0; i < input.Length; i++)
            {
                instructions.Add(input[i]);
            }
            instructions.Add(17);
            instructions.Add(31);
            instructions.Add(73);
            instructions.Add(47);
            instructions.Add(23);

            var sparseHash = RunRounds(256, instructions, 64);
            var denseHash = GetDenseHash(sparseHash);
            return denseHash;
        }

        public static string KnotHash(string input)
        {
            var denseHash = KnotHashBytes(input);
            return ToHex(denseHash);
        }

        private static string ToHex(byte[] denseHash)
        {
            char[] str = new char[denseHash.Length * 2];
            for (int i = 0; i < denseHash.Length; i++)
            {
                byte b = denseHash[i];
                for (int nibble = 0; nibble < 2; nibble++)
                {
                    var n = (b >> ((1 - nibble) * 4)) & 0xF;
                    if (n < 10)
                    {
                        str[i * 2 + nibble] = (char)('0' + n);
                    }
                    else
                    {
                        str[i * 2 + nibble] = (char)('a' + n - 10);
                    }
                }
            }

            return new string(str);
        }

        private static byte[] GetDenseHash(byte[] sparseHash)
        {
            var result = new byte[sparseHash.Length / 16];

            for (int i = 0; i < result.Length; i++)
            {
                byte r = sparseHash[i * 16];
                for (int j = i * 16 + 1; j < i * 16 + 16; j++)
                {
                    r = (byte)(r ^ sparseHash[j]);
                }
                result[i] = r;
            }

            return result;
        }

        public static byte[] GetResult(int size, string input)
        {
            var instructions = input.Split(',').Select(int.Parse).ToList();
            return RunRounds(size, instructions, 1);

        }
        private static byte[] RunRounds(int size, List<int> input, int rounds)
        {
            byte[] list = new byte[size];
            for (int i = 0; i < size; i++)
            {
                list[i] = (byte)i;
            }

            int skipSize = 0;
            int position = 0;

            for (int round = 0; round < rounds; round++)
            {
                foreach (var inst in input)
                {
                    // reverse list between position and position + inst.
                    for (int start = position, end = position + inst - 1; start < end; start++, end--)
                    {
                        byte temp = list[start % size];
                        list[start % size] = list[end % size];
                        list[end % size] = temp;
                    }

                    position += inst + skipSize;
                    skipSize++;
                }
            }

            return list;
        }
    }
}
