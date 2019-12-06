using System;
using System.IO;

namespace aoc.csharp._2019
{
    public class Day02 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var list = Input.To<int[]>(input);

            var part1 = (int[])list.Clone();
            part1[1] = 12;
            part1[2] = 2;
            RunProgram(part1);

            var part2 = new int[list.Length];
            for (int verb = 0; verb < 100; verb++)
            {
                for (int noun = 0; noun < 100; noun++)
                {
                    Array.Copy(list, part2, list.Length);
                    part2[1] = noun;
                    part2[2] = verb;
                    RunProgram(part2);

                    if (part2[0] == 19690720)
                    {
                        return (part1[0].ToString(), (100 * noun + verb).ToString());
                    }
                }
            }

            throw new Exception("Couldn't find part2 answer");
        }

        public static void RunProgram(int[] memory)
        {
            int ip = 0; //instruction pointer

            while (true)
            {
                var opcode = memory[ip];
                var instruction_length = 0;
                switch (opcode)
                {
                    case 1:
                        {
                            instruction_length = 4;
                            var addr1 = memory[ip + 1];
                            var addr2 = memory[ip + 2];
                            var addr3 = memory[ip + 3];
                            var val1 = memory[addr1];
                            var val2 = memory[addr2];
                            memory[addr3] = val1 + val2;
                        }
                        break;

                    case 2:
                        {
                            instruction_length = 4;
                            var addr1 = memory[ip + 1];
                            var addr2 = memory[ip + 2];
                            var addr3 = memory[ip + 3];
                            var val1 = memory[addr1];
                            var val2 = memory[addr2];
                            memory[addr3] = val1 * val2;
                        }
                        break;

                    case 99:
                        return;

                    default:
                        throw new NotSupportedException($"Opcode {opcode} unknown");
                }
                ip += instruction_length;
            }
        }
    }
}
