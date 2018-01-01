using System;
using System.Collections.Generic;
using System.IO;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day18
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day18(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(18);
        }

        [Fact]
        public void Sample()
        {
            const string part1Input = @"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";
            var programOutput = Part1(part1Input);

            Assert.NotEmpty(programOutput);
            Assert.Equal(4, programOutput[programOutput.Count - 1]);

            const string part2Input = @"snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d";

            programOutput = Part2(part2Input);
            Assert.Equal(3, programOutput.Count);
            Assert.Equal(1, programOutput[0]);
            Assert.Equal(2, programOutput[1]);
            Assert.Equal(1, programOutput[2]);
        }

        [Fact]
        public void Puzzle()
        {
            var programOutput = Part1(_input);
            _output.WriteLine("part 1 = {0}", programOutput[programOutput.Count - 1]);
            programOutput = Part2(_input);
            _output.WriteLine("part2 = {0}", programOutput.Count);
        }

        private List<long> Part1(string input)
        {
            var instructions = GetInstructions(input);
            var programInput = new List<long>();
            var programOutput = new List<long>();
            var program = new Program(0, instructions, programInput, programOutput);

            while (program.Step()) ;

            return programOutput;
        }

        private List<long> Part2(string input)
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

        private List<string[]> GetInstructions(string input)
        {
            var instructions = new List<string[]>();

            using (var reader = new StringReader(input))
            {
                string line;
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
            private List<long> _input;
            private int _inputIndex;
            private List<long> _output;
            private List<string[]> _instructions;
            private int _pc;
            private Registers _registers;

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
            private Dictionary<char, long> _registers = new Dictionary<char, long>();

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
