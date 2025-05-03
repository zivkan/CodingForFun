using System;
using System.Collections.Generic;

namespace aoc.csharp._2019;

public class IntcodeVm
{
    public IQueue<long> Input { get; }
    public IQueue<long> Output { get; }

    private long _instructionPointer;
    private readonly Dictionary<long, long> _memory;
    private long _relativeBase;

    public IntcodeVm(IList<long> program)
        : this(program, new StandardQueue<long>(), new StandardQueue<long>())
    {
    }

    public IntcodeVm(IList<long> program, IQueue<long> input, IQueue<long> output)
    {
        _memory = new Dictionary<long, long>(program.Count);
        for (int i = 0; i < program.Count; i++)
        {
            _memory[i] = program[i];
        }

        _instructionPointer = 0;
        _relativeBase = 0;
        Input = input;
        Output = output;
    }

    public bool Step()
    {
        var instruction = _memory[_instructionPointer];
        var opcode = instruction % 100;
        var modes = instruction / 100;
        long instruction_length;
        switch (opcode)
        {
            case 1: // add
                {
                    instruction_length = 4;
                    var arg1 = _memory[_instructionPointer + 1];
                    var mode1 = modes % 10;
                    var arg2 = _memory[_instructionPointer + 2];
                    var mode2 = (modes / 10) % 10;
                    var arg3 = _memory[_instructionPointer + 3];
                    var mode3 = (modes / 100) % 10;
                    var val1 = GetValue(arg1, mode1);
                    var val2 = GetValue(arg2, mode2);
                    SetValue(arg3, mode3, val1 + val2);
                }
                break;

            case 2: // mult
                {
                    instruction_length = 4;
                    var arg1 = _memory[_instructionPointer + 1];
                    var mode1 = modes % 10;
                    var arg2 = _memory[_instructionPointer + 2];
                    var mode2 = (modes / 10) % 10;
                    var arg3 = _memory[_instructionPointer + 3];
                    var mode3 = (modes / 100) % 10;
                    var val1 = GetValue(arg1, mode1);
                    var val2 = GetValue(arg2, mode2);
                    SetValue(arg3, mode3, val1 * val2);
                }
                break;

            case 3: // in
                {
                    instruction_length = 2;
                    var addr = _memory[_instructionPointer + 1];
                    var mode = modes % 10;
                    if (!Input.TryDequeue(out var value))
                    {
                        throw new Exception("No input available");
                    }
                    SetValue(addr, mode, value);
                }
                break;

            case 4: // out
                {
                    instruction_length = 2;
                    var arg = _memory[_instructionPointer + 1];
                    var mode = modes % 10;
                    var value = GetValue(arg, mode);
                    Output.Enqueue(value);
                }
                break;

            case 5: // jump if true
                {
                    var arg1 = _memory[_instructionPointer + 1];
                    var mode1 = modes % 10;
                    var value1 = GetValue(arg1, mode1);
                    if (value1 != 0)
                    {
                        var arg2 = _memory[_instructionPointer + 2];
                        var mode2 = (modes / 10) % 10;
                        var value2 = GetValue(arg2, mode2);
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
                    var value1 = GetValue(arg1, mode1);
                    if (value1 == 0)
                    {
                        var arg2 = _memory[_instructionPointer + 2];
                        var mode2 = (modes / 10) % 10;
                        var value2 = GetValue(arg2, mode2);
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
                    var arg3 = _memory[_instructionPointer + 3];
                    var mode3 = (modes / 100) % 10;
                    var val1 = GetValue(arg1, mode1);
                    var val2 = GetValue(arg2, mode2);
                    SetValue(arg3, mode3, val1 < val2 ? 1 : 0);
                }
                break;

            case 8: // equal
                {
                    instruction_length = 4;
                    var arg1 = _memory[_instructionPointer + 1];
                    var mode1 = modes % 10;
                    var arg2 = _memory[_instructionPointer + 2];
                    var mode2 = (modes / 10) % 10;
                    var arg3 = _memory[_instructionPointer + 3];
                    var mode3 = (modes / 100) % 10;
                    var val1 = GetValue(arg1, mode1);
                    var val2 = GetValue(arg2, mode2);
                    SetValue(arg3, mode3, val1 == val2 ? 1 : 0);
                }
                break;

            case 9: // set relative base
                {
                    instruction_length = 2;
                    var arg = _memory[_instructionPointer + 1];
                    var mode = modes % 10;
                    var val = GetValue(arg, mode);
                    _relativeBase += val;
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

    private void SetValue(long address, long mode, long value)
    {
        var destination = mode switch
        {
            0 => address,
            2 => _relativeBase + address,
            _ => throw new NotSupportedException("Mode " + mode + " not supported"),
        };
        _memory[destination] = value;
    }

    public long GetMemory(int address)
    {
        return _memory[address];
    }

    private long GetValue(long arg, long mode)
    {
        long GetMemory(long addr)
        {
            if (_memory.TryGetValue(addr, out var value))
            {
                return value;
            }
            return 0;
        }

        return mode switch
        {
            0 => GetMemory(arg),
            1 => arg,
            2 => GetMemory(_relativeBase + arg),
            _ => throw new Exception("mode " + mode + " not supported"),
        };
    }
}
