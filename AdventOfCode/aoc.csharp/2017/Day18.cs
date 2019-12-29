using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2017
{
    public class Day18 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var programOutput = Part1(text);
            var part1 = programOutput[programOutput.Count - 1];
            programOutput = Part2(text);
            var part2 = programOutput.Count;

            return (part1.ToString(), part2.ToString());
        }

        public static List<long> Part1(string input)
        {
            var instructions = GetInstructions(input);
            var programInput = new List<long>();
            var programOutput = new List<long>();
            var program = new Program(0, instructions, programInput, programOutput);

            while (program.Step()) ;

            return programOutput;
        }

        public static List<long> Part2(string input)
        {
            var instructions = GetInstructions(input);
            var program0Output = new List<long>();
            var program1Output = new List<long>();
            var running = new Program(0, instructions, program1Output, program0Output);
            var waiting = new Program(1, instructions, program0Output, program1Output);

            bool deadlock = false;
            while (!deadlock)
            {
                if (!running.Step())
                {
                    if (waiting.Step())
                    {
                        var swap = running;
                        running = waiting;
                        waiting = swap;
                    }
                    else
                    {
                        deadlock = true;
                    }
                }
            }

            return program1Output;
        }

        private static List<string[]> GetInstructions(string input)
        {
            var instructions = new List<string[]>();

            using (var reader = new StringReader(input))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var instruction = line.Split(' ');
                    instructions.Add(instruction);
                }
            }

            return instructions;
        }

        private class Program
        {
            private readonly List<long> _input;
            private int _inputIndex;
            private readonly List<long> _output;
            private readonly List<string[]> _instructions;
            private int _pc;
            private readonly Registers _registers;

            public Program(int pid, List<string[]> instructions, List<long> input, List<long> output)
            {
                _input = input;
                _inputIndex = 0;
                _output = output;
                _instructions = instructions;
                _pc = 0;
                _registers = new Registers();
                _registers['p'] = pid;
            }

            public bool Step()
            {
                if (_pc < 0 || _pc >= _instructions.Count)
                {
                    return false;
                }

                var instruction = _instructions[_pc];

                long value;
                switch (instruction[0])
                {
                    case "set":
                        value = GetValue(instruction[2]);
                        _registers[instruction[1][0]] = value;
                        break;

                    case "add":
                        value = GetValue(instruction[2]);
                        _registers[instruction[1][0]] += value;
                        break;

                    case "mul":
                        value = GetValue(instruction[2]);
                        _registers[instruction[1][0]] *= value;
                        break;

                    case "mod":
                        value = GetValue(instruction[2]);
                        _registers[instruction[1][0]] %= value;
                        break;

                    case "snd":
                        value = GetValue(instruction[1]);
                        _output.Add(value);
                        break;

                    case "rcv":
                        if (_inputIndex >= _input.Count)
                        {
                            return false;
                        }
                        value = _input[_inputIndex];
                        _inputIndex++;
                        _registers[instruction[1][0]] = value;
                        break;

                    case "jgz":
                        value = GetValue(instruction[1]);
                        if (value > 0)
                        {
                            value = GetValue(instruction[2]);
                            _pc += (int)value - 1;
                        }
                        break;

                    default:
                        throw new NotImplementedException($"opcode '{instruction[0]}' not implemented");
                }

                _pc++;
                return true;
            }

            private long GetValue(string v)
            {
                if (v.Length == 1 && v[0] >= 'a' && v[0] < 'z')
                {
                    long value = _registers[v[0]];
                    return value;
                }

                return long.Parse(v);
            }
        }

        private class Registers
        {
            private readonly Dictionary<char, long> _registers = new Dictionary<char, long>();

            public long this[char register]
            {
                get
                {
                    if (register < 'a' && register > 'z')
                    {
                        throw new ArgumentException();
                    }

                    if (_registers.TryGetValue(register, out var value))
                    {
                        return value;
                    }

                    return 0;
                }
                set
                {
                    if (register < 'a' && register > 'z')
                    {
                        throw new ArgumentException();
                    }

                    _registers[register] = value;
                }
            }
        }
    }
}
