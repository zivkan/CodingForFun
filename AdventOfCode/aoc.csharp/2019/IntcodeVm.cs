using System;
using System.Collections.Generic;

namespace aoc.csharp._2019
{
    public class IntcodeVm
    {
        public Queue<int> Input { get; }
        public Queue<int> Output { get; }

        private int _instructionPointer;
        private int[] _memory;

        public IntcodeVm(int[] program)
        {
            _memory = (int[])program.Clone();

            _instructionPointer = 0;
            Input = new Queue<int>();
            Output = new Queue<int>();
        }

        public bool Step()
        {
            var instruction = _memory[_instructionPointer];
            var opcode = instruction % 100;
            var modes = instruction / 100;
            int instruction_length;
            switch (opcode)
            {
                case 1: // add
                    {
                        instruction_length = 4;
                        var arg1 = _memory[_instructionPointer + 1];
                        var mode1 = modes % 10;
                        var arg2 = _memory[_instructionPointer + 2];
                        var mode2 = (modes / 10) % 10;
                        var addr3 = _memory[_instructionPointer + 3];
                        var val1 = GetValue(arg1, mode1, _memory);
                        var val2 = GetValue(arg2, mode2, _memory);
                        _memory[addr3] = val1 + val2;
                    }
                    break;

                case 2: // mult
                    {
                        instruction_length = 4;
                        var arg1 = _memory[_instructionPointer + 1];
                        var mode1 = modes % 10;
                        var arg2 = _memory[_instructionPointer + 2];
                        var mode2 = (modes / 10) % 10;
                        var addr3 = _memory[_instructionPointer + 3];
                        var val1 = GetValue(arg1, mode1, _memory);
                        var val2 = GetValue(arg2, mode2, _memory);
                        _memory[addr3] = val1 * val2;
                    }
                    break;

                case 3: // in
                    {
                        instruction_length = 2;
                        var addr = _memory[_instructionPointer + 1];
                        if (!Input.TryDequeue(out var value))
                        {
                            throw new Exception("No input available");
                        }
                        _memory[addr] = value;
                    }
                    break;

                case 4: // out
                    {
                        instruction_length = 2;
                        var arg = _memory[_instructionPointer + 1];
                        var mode = modes % 10;
                        var value = GetValue(arg, mode, _memory);
                        Output.Enqueue(value);
                    }
                    break;

                case 5: // jump if true
                    {
                        var arg1 = _memory[_instructionPointer + 1];
                        var mode1 = modes % 10;
                        var value1 = GetValue(arg1, mode1, _memory);
                        if (value1 != 0)
                        {
                            var arg2 = _memory[_instructionPointer + 2];
                            var mode2 = (modes / 10) % 10;
                            var value2 = GetValue(arg2, mode2, _memory);
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
                        var arg1 = _memory[_instructionPointer + 1];
                        var mode1 = modes % 10;
                        var value1 = GetValue(arg1, mode1, _memory);
                        if (value1 == 0)
                        {
                            var arg2 = _memory[_instructionPointer + 2];
                            var mode2 = (modes / 10) % 10;
                            var value2 = GetValue(arg2, mode2, _memory);
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
                        var arg1 = _memory[_instructionPointer + 1];
                        var mode1 = modes % 10;
                        var arg2 = _memory[_instructionPointer + 2];
                        var mode2 = (modes / 10) % 10;
                        var addr3 = _memory[_instructionPointer + 3];
                        var val1 = GetValue(arg1, mode1, _memory);
                        var val2 = GetValue(arg2, mode2, _memory);
                        _memory[addr3] = val1 < val2 ? 1 : 0;
                    }
                    break;

                case 8: // equal
                    {
                        instruction_length = 4;
                        var arg1 = _memory[_instructionPointer + 1];
                        var mode1 = modes % 10;
                        var arg2 = _memory[_instructionPointer + 2];
                        var mode2 = (modes / 10) % 10;
                        var addr3 = _memory[_instructionPointer + 3];
                        var val1 = GetValue(arg1, mode1, _memory);
                        var val2 = GetValue(arg2, mode2, _memory);
                        _memory[addr3] = val1 == val2 ? 1 : 0;
                    }
                    break;

                case 99:
                    return false;

                default:
                    throw new NotSupportedException($"Opcode {opcode} unknown");
            }

            if (instruction_length > 0)
            {
                _instructionPointer += instruction_length;
            }
            else
            {
                _instructionPointer = -instruction_length;
            }

            return true;
        }

        public int GetMemory(int address)
        {
            return _memory[address];
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
