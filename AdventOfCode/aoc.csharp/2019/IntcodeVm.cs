using System;
using System.Collections.Generic;

namespace aoc.csharp._2019
{
    public class IntcodeVm
    {
        public static Queue<int> RunProgram(int[] memory, Queue<int> input)
        {
            int ip = 0; //instruction pointer
            var output = new Queue<int>();

            while (true)
            {
                var instruction = memory[ip];
                var opcode = instruction % 100;
                var modes = instruction / 100;
                int instruction_length;
                switch (opcode)
                {
                    case 1: // add
                        {
                            instruction_length = 4;
                            var arg1 = memory[ip + 1];
                            var mode1 = modes % 10;
                            var arg2 = memory[ip + 2];
                            var mode2 = (modes / 10) % 10;
                            var addr3 = memory[ip + 3];
                            var val1 = GetValue(arg1, mode1, memory);
                            var val2 = GetValue(arg2, mode2, memory);
                            memory[addr3] = val1 + val2;
                        }
                        break;

                    case 2: // mult
                        {
                            instruction_length = 4;
                            var arg1 = memory[ip + 1];
                            var mode1 = modes % 10;
                            var arg2 = memory[ip + 2];
                            var mode2 = (modes / 10) % 10;
                            var addr3 = memory[ip + 3];
                            var val1 = GetValue(arg1, mode1, memory);
                            var val2 = GetValue(arg2, mode2, memory);
                            memory[addr3] = val1 * val2;
                        }
                        break;

                    case 3: // in
                        {
                            instruction_length = 2;
                            var addr = memory[ip + 1];
                            if (!input.TryDequeue(out var value))
                            {
                                throw new Exception("No input available");
                            }
                            memory[addr] = value;
                        }
                        break;

                    case 4: // out
                        {
                            instruction_length = 2;
                            var arg = memory[ip + 1];
                            var mode = modes % 10;
                            var value = GetValue(arg, mode, memory);
                            output.Enqueue(value);
                        }
                        break;

                    case 5: // jump if true
                        {
                            var arg1 = memory[ip + 1];
                            var mode1 = modes % 10;
                            var value1 = GetValue(arg1, mode1, memory);
                            if (value1 != 0)
                            {
                                var arg2 = memory[ip + 2];
                                var mode2 = (modes / 10) % 10;
                                var value2 = GetValue(arg2, mode2, memory);
                                instruction_length = -value2;
                            }
                            else
                            {
                                instruction_length = 3;
                            }
                        }
                        break;

                    case 6: // jump if false
                        {
                            var arg1 = memory[ip + 1];
                            var mode1 = modes % 10;
                            var value1 = GetValue(arg1, mode1, memory);
                            if (value1 == 0)
                            {
                                var arg2 = memory[ip + 2];
                                var mode2 = (modes / 10) % 10;
                                var value2 = GetValue(arg2, mode2, memory);
                                instruction_length = -value2;
                            }
                            else
                            {
                                instruction_length = 3;
                            }
                        }
                        break;

                    case 7: // less than
                        {
                            instruction_length = 4;
                            var arg1 = memory[ip + 1];
                            var mode1 = modes % 10;
                            var arg2 = memory[ip + 2];
                            var mode2 = (modes / 10) % 10;
                            var addr3 = memory[ip + 3];
                            var val1 = GetValue(arg1, mode1, memory);
                            var val2 = GetValue(arg2, mode2, memory);
                            memory[addr3] = val1 < val2 ? 1 : 0;
                        }
                        break;

                    case 8: // equal
                        {
                            instruction_length = 4;
                            var arg1 = memory[ip + 1];
                            var mode1 = modes % 10;
                            var arg2 = memory[ip + 2];
                            var mode2 = (modes / 10) % 10;
                            var addr3 = memory[ip + 3];
                            var val1 = GetValue(arg1, mode1, memory);
                            var val2 = GetValue(arg2, mode2, memory);
                            memory[addr3] = val1 == val2 ? 1 : 0;
                        }
                        break;

                    case 99:
                        return output;

                    default:
                        throw new NotSupportedException($"Opcode {opcode} unknown");
                }

                if (instruction_length > 0)
                {
                    ip += instruction_length;
                }
                else
                {
                    ip = -instruction_length;
                }
            }
        }

        private static int GetValue(int arg, int mode, int[] memory)
        {
            if (mode > 0)
            {
                return arg;
            }
            else
            {
                return memory[arg];
            }
        }
    }
}
