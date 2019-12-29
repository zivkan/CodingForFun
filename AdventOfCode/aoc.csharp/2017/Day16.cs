using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2017
{
    public class Day16 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            List<int[]> dances = new List<int[]>();
            var programs = Enumerable.Range(0, 16).ToArray();
            var dance = Parse(text, programs.Length);
            const int oneBillion = 1_000_000_000;
            do
            {
                dances.Add((int[])programs.Clone());
                RunDance(programs, dance);
            } while (!IsStartPositions(programs) && dances.Count <= oneBillion);

            var part1 = ToString(dances[1]);

            int offset = 1_000_000_000 % dances.Count;
            var part2 = ToString(dances[offset]);

            return (part1, part2);
        }

        public static bool IsStartPositions(int[] next)
        {
            for (int i = 0; i < next.Length; i++)
            {
                if (next[i] != i)
                {
                    return false;
                }
            }

            return true;
        }

        public static List<IMove> Parse(string input, int length)
        {
            var moves = new List<IMove>();
            foreach (var moveInstruction in input.Split(','))
            {
                var opcode = moveInstruction[0];
                if (opcode == 's')
                {
                    int positions = int.Parse(moveInstruction.Substring(1));
                    var spin = new Spin(length, positions);
                    moves.Add(spin);
                }
                else if (opcode == 'x')
                {
                    int slashIndex = moveInstruction.IndexOf('/');
                    var firstSubStr = moveInstruction[1..slashIndex];
                    var firstValue = int.Parse(firstSubStr);
                    var secondSubStr = moveInstruction.Substring(slashIndex + 1);
                    var secondValue = int.Parse(secondSubStr);
                    var exchange = new Exchange(firstValue, secondValue);
                    moves.Add(exchange);
                }
                else if (opcode == 'p')
                {
                    var partner = new Partner(moveInstruction[1], moveInstruction[3]);
                    moves.Add(partner);
                }
                else
                {
                    throw new ArgumentException("invalid opcode for line " + moveInstruction);
                }
            }

            return moves;
        }

        public static void RunDance(int[] programs, IEnumerable<IMove> dance)
        {
            foreach (var move in dance)
            {
                move.Move(programs);
            }
        }

        public static string ToString(int[] programs)
        {
            char[] chars = new char[programs.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)('a' + programs[i]);
            }

            return new string(chars);
        }

        public interface IMove
        {
            void Move(int[] programs);
        }

        private class Spin : IMove
        {
            private readonly int expectedLength;
            private readonly int[] temp;

            public Spin(int expectedLength, int spinPositions)
            {
                this.expectedLength = expectedLength;
                temp = new int[expectedLength - spinPositions];
            }

            public void Move(int[] programs)
            {
                if (programs.Length != expectedLength)
                {
                    throw new ArgumentException();
                }

                Array.Copy(programs, 0, temp, 0, temp.Length);
                Array.Copy(programs, temp.Length, programs, 0, expectedLength - temp.Length);
                Array.Copy(temp, 0, programs, expectedLength - temp.Length, temp.Length);
            }
        }

        private class Exchange : IMove
        {
            private readonly int x;
            private readonly int y;

            public Exchange(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public void Move(int[] programs)
            {
                int temp = programs[x];
                programs[x] = programs[y];
                programs[y] = temp;
            }
        }

        private class Partner : IMove
        {
            private readonly int x;
            private readonly int y;

            public Partner(char x, char y)
            {
                this.x = x - 'a';
                this.y = y - 'a';
            }

            public void Move(int[] programs)
            {
                int xPos = -1;
                int yPos = -1;

                for (int i = 0; i < programs.Length; i++)
                {
                    if (programs[i] == x)
                    {
                        xPos = i;
                        if (yPos >= 0)
                        {
                            break;
                        }
                    }
                    if (programs[i] == y)
                    {
                        yPos = i;
                        if (xPos >= 0)
                        {
                            break;
                        }
                    }
                }

                if (xPos == -1 || yPos == -1)
                {
                    throw new Exception();
                }

                int tmp = programs[xPos];
                programs[xPos] = programs[yPos];
                programs[yPos] = tmp;
            }
        }
    }
}
