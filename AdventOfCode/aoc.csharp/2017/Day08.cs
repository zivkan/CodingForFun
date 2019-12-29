using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2017
{
    public class Day08 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var (registers, part2) = RunProgram(text);
            var part1 = registers.Values.Max();
            return (part1.ToString(), part2.ToString());
        }

        public static (Dictionary<string, int> registers, int maxValue) RunProgram(string input)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            int maxValue = 0;
            using (var reader = new StringReader(input))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Instruction instruction = new Instruction(line);

                    if (!registers.TryGetValue(instruction.SourceRegister, out int srcValue))
                    {
                        srcValue = 0;
                    }


                    if (CheckComparison(srcValue, instruction.ComparisonOperator, instruction.ComparisonValue))
                    {
                        if (!registers.TryGetValue(instruction.DestinationRegister, out int destValue))
                        {
                            destValue = 0;
                        }

                        destValue = IncrementValue(destValue, instruction.IncrementAmount, instruction.IncrementDestination);
                        registers[instruction.DestinationRegister] = destValue;
                        if (destValue > maxValue)
                        {
                            maxValue = destValue;
                        }
                    }
                }
            }
            return (registers, maxValue);
        }

        private static int IncrementValue(int destValue, int incrementAmount, bool incrementDestination)
        {
            return incrementDestination
                ? destValue + incrementAmount
                : destValue - incrementAmount;
        }

        private static bool CheckComparison(int srcValue, string op, int compareValue)
        {
            return op switch
            {
                ">" => srcValue > compareValue,
                "<" => srcValue < compareValue,
                ">=" => srcValue >= compareValue,
                "<=" => srcValue <= compareValue,
                "==" => srcValue == compareValue,
                "!=" => srcValue != compareValue,
                _ => throw new ArgumentException($"Unknown comparison operator {op}"),
            };
        }

        private class Instruction
        {
            private static readonly Regex _regex = new Regex("^(?<dest>[a-z]+) (?<inc>inc|dec) (?<amount>[-\\d]+) if (?<src>[a-z]+) (?<op>>|<|>=|<=|==|!=) (?<comp>[-\\d]+)$");

            public Instruction(string line)
            {
                Match match = _regex.Match(line);
                if (!match.Success)
                {
                    throw new ArgumentException(line);
                }

                var groups = match.Groups;

                DestinationRegister = groups["dest"].Value;
                IncrementDestination = CheckIncrementValue(groups["inc"].Value);
                IncrementAmount = int.Parse(groups["amount"].Value);
                SourceRegister = groups["src"].Value;
                ComparisonOperator = groups["op"].Value;
                ComparisonValue = int.Parse(groups["comp"].Value);
            }

            public string DestinationRegister { get; }
            public bool IncrementDestination { get; }
            public int IncrementAmount { get; }
            public string SourceRegister { get; }
            public string ComparisonOperator { get; }
            public int ComparisonValue { get; }

            private bool CheckIncrementValue(string value)
            {
                return value switch
                {
                    "inc" => true,
                    "dec" => false,
                    _ => throw new ArgumentException($"Unknown increment value \"{value}\""),
                };
            }
        }
    }
}
