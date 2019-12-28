using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2018
{
    public class Day16 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var (samples, program) = ParseInput(input);
            var results = samples.Select(s => new KeyValuePair<Sample, HashSet<string>>(s, Possibilities(s))).ToList();
            var part1 = results.Count(r => r.Value.Count >= 3);

            var mapping = GetInstructionMapping(results);

            var registers = new int[] { 0, 0, 0, 0 };
            for (int i = 0; i < program.Count; i++)
            {
                var instruction = program[i];
                var opCodeName = mapping[instruction.Opcode];
                _instructions[opCodeName](instruction, registers);
            }

            var part2 = registers[0];

            return (part1.ToString(), part2.ToString());
        }

        private readonly static Regex _beforeRegex = new Regex(@"Before: \[(\d+), (\d+), (\d+), (\d+)\]");
        private readonly static Regex _instRegex = new Regex(@"(\d+) (\d+) (\d+) (\d+)");
        private readonly static Regex _afterRegex = new Regex(@"After:  \[(\d+), (\d+), (\d+), (\d+)\]");
        private readonly static Dictionary<string, Action<Instruction, int[]>> _instructions = GetInstructions();

        private static (List<Sample>, List<Instruction>) ParseInput(TextReader reader)
        {
            var samples = new List<Sample>();
            var instructions = new List<Instruction>();

            string? line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                var match = _beforeRegex.Match(line);
                if (!match.Success) throw new Exception("doesn't match before regex");
                var before = new[] { int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value) };
                line = reader.ReadLine();
                match = _instRegex.Match(line);
                if (!match.Success) throw new Exception("doesn't match instr regex");
                var instr = new Instruction(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
                line = reader.ReadLine();
                match = _afterRegex.Match(line);
                if (!match.Success) throw new Exception("doesn't match after regex");
                var after = new int[] { int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value) };
                _ = reader.ReadLine();

                var sample = new Sample(instr, Array.AsReadOnly(before), Array.AsReadOnly(after));
                samples.Add(sample);
            }

            while (string.IsNullOrEmpty(line)) line = reader.ReadLine();


            while ((line = reader.ReadLine()) != null)
            {
                var match = _instRegex.Match(line);
                if (!match.Success) throw new Exception("bad instruction");
                var instr = new Instruction(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
                instructions.Add(instr);
            }

            return (samples, instructions);
        }

        public static HashSet<string> Possibilities(Sample input)
        {
            var possibilities = new HashSet<string>();

            foreach (var instr in _instructions)
            {
                var registers = input.Before.ToArray();
                instr.Value(input.Instruction, registers);
                var expected = input.After;
                if (expected[0] == registers[0] &&
                    expected[1] == registers[1] &&
                    expected[2] == registers[2] &&
                    expected[3] == registers[3])
                {
                    possibilities.Add(instr.Key);
                }
            }

            return possibilities;
        }

        private static Dictionary<int, string> GetInstructionMapping(List<KeyValuePair<Sample, HashSet<string>>> results)
        {
            var opcodes = _instructions.Keys.ToList();

            var possibilities = results.Select(r => r.Key.Instruction.Opcode)
                .Distinct()
                .ToDictionary(r => r, _ =>
                {
                    var set = new HashSet<string>(_instructions.Count);
                    foreach (var code in opcodes) set.Add(code);
                    return set;
                });
            var toRemove = new List<string>(_instructions.Count);
            foreach (var r in results)
            {
                var set = possibilities[r.Key.Instruction.Opcode];
                toRemove.Clear();
                foreach (var code in set)
                {
                    toRemove.Add(code);
                }
                foreach (var code in r.Value)
                {
                    toRemove.Remove(code);
                }
                foreach (var code in toRemove)
                {
                    set.Remove(code);
                }
            }

            var mapping = new Dictionary<int, string>();
            while (possibilities.Count > 0)
            {
                var selected = possibilities.First(p => p.Value.Count == 1);
                var value = selected.Value.Single();
                mapping[selected.Key] = value;
                possibilities.Remove(selected.Key);

                foreach (var possibility in possibilities)
                {
                    possibility.Value.Remove(value);
                }
            }

            return mapping;
        }

        private static Dictionary<string, Action<Instruction, int[]>> GetInstructions()
        {
            var result = new Dictionary<string, Action<Instruction, int[]>>()
            {
                ["addr"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] + registers[instr.B],
                ["addi"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] + instr.B,
                ["mulr"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] * registers[instr.B],
                ["muli"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] * instr.B,
                ["banr"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] & registers[instr.B],
                ["bani"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] & instr.B,
                ["borr"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] | registers[instr.B],
                ["bori"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] | instr.B,
                ["setr"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A],
                ["seti"] = (Instruction instr, int[] registers) => registers[instr.C] = instr.A,
                ["gtir"] = (Instruction instr, int[] registers) => registers[instr.C] = instr.A > registers[instr.B] ? 1 : 0,
                ["gtri"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] > instr.B ? 1 : 0,
                ["gtrr"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] > registers[instr.B] ? 1 : 0,
                ["eqir"] = (Instruction instr, int[] registers) => registers[instr.C] = instr.A == registers[instr.B] ? 1 : 0,
                ["eqri"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] == instr.B ? 1 : 0,
                ["eqrr"] = (Instruction instr, int[] registers) => registers[instr.C] = registers[instr.A] == registers[instr.B] ? 1 : 0,
            };

            return result;
        }

        public class Instruction
        {
            public int Opcode { get; }
            public int A { get; }
            public int B { get; }
            public int C { get; }

            public Instruction(int opcode, int a, int b, int c)
            {
                Opcode = opcode;
                A = a;
                B = b;
                C = c;
            }
        }

        public class Sample
        {
            public Instruction Instruction { get; }
            public IReadOnlyList<int> Before { get; }
            public IReadOnlyList<int> After { get; }

            public Sample(Instruction instruction, IReadOnlyList<int> before, IReadOnlyList<int> after)
            {
                Instruction = instruction;
                Before = before;
                After = after;
            }
        }
    }
}
