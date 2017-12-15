using System;
using System.Collections.Generic;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day10
    {
        private ITestOutputHelper _output;
        private string _input;

        private const string _sample = "3,4,1,5";

        public Day10(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(10);
        }

        [Fact]
        public void Part1Sample()
        {
            var list = GetResult(5, _sample);
            int result = list[0] * list[1];
            Assert.Equal(12, result);
        }

        [Fact]
        public void Part1()
        {
            var list = GetResult(256, _input);
            int result = list[0] * list[1];
            _output.WriteLine("Result = {0}", result);
        }

        [Theory]
        [InlineData("", "a2582a3a0e66e6e86e3812dcb672a272")]
        [InlineData("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd")]
        [InlineData("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d")]
        [InlineData("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e")]
        public void Part2Sample(string input, string expected)
        {
            string hash = KnotHash(input);
            Assert.Equal(expected, hash);
        }

        [Fact]
        public void Part2()
        {
            string hash = KnotHash(_input);
            _output.WriteLine("Knot hash = " + hash);
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

        private byte[] GetResult(int size, string input)
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
