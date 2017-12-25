using System;
using System.Collections.Generic;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day16
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day16(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(16);
        }

        [Fact]
        public void Sample()
        {
            var programs = Enumerable.Range(0, 5).ToArray();
            var dance = Parse("s1,x3/4,pe/b", programs.Length);
            RunDance(programs, dance);
            var result = ToString(programs);
            Assert.Equal("baedc", result);
        }

        [Fact]
        public void Puzzle()
        {
            List<int[]> dances = new List<int[]>();
            var programs = Enumerable.Range(0, 16).ToArray();
            var dance = Parse(_input, programs.Length);
            do
            {
                dances.Add((int[])programs.Clone());
                RunDance(programs, dance);
            } while (!IsStartPositions(programs));

            var result = ToString(programs);
            _output.WriteLine("Part 1: {0}", result);

            int offset = 1_000_000_000 % dances.Count;
            result = ToString(dances[offset]);
            _output.WriteLine("Part 2: {0}", result);
        }

        private bool IsStartPositions(int[] next)
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

        private int[] ApplyTransform(int[] transform, int[] initial)
        {
            if (transform.Length != initial.Length)
            {
                throw new ArgumentException();
            }

            var result = new int[transform.Length];
            for (int i = 0; i < transform.Length; i++)
            {
                result[i] = initial[transform[i]];
            }

            return result;
        }

        private List<IMove> Parse(string input, int length)
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
                    var firstSubStr = moveInstruction.Substring(1, slashIndex - 1);
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

        private void RunDance(int[] programs, IEnumerable<IMove> dance)
        {
            foreach (var move in dance)
            {
                move.Move(programs);
            }
        }

        private string ToString(int[] programs)
        {
            char[] chars = new char[programs.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)('a' + programs[i]);
            }

            return new string(chars);
        }

        private interface IMove
        {
            void Move(int[] programs);
        }

        private class Spin: IMove
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
            private int x;
            private int y;

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
            private int x;
            private int y;

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
