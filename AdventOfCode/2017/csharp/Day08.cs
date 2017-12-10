using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day08
    {
        private ITestOutputHelper _output;
        private string _input;

        private const string _sample = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";

        public Day08(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(8);
        }

        [Fact]
        public void SampleInput()
        {
            var (registers,maxValue) = RunProgram(_sample);
            var highestRegister = registers.Values.Max();
            Assert.Equal(1, highestRegister);
            Assert.Equal(10, maxValue);
        }

        [Fact]
        public void PuzzleInput()
        {
            var (registers,maxValue) = RunProgram(_input);
            var maxRegister = registers.Values.Max();
            _output.WriteLine("Max register = {0}", maxRegister);
            _output.WriteLine("Max value = {0}", maxValue);
        }

        private (Dictionary<string,int>,int) RunProgram(string input)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            int maxValue = 0;
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Instruction instruction = new Instruction(line);

                    int srcValue;
                    if (!registers.TryGetValue(instruction.SourceRegister, out srcValue))
                    {
                        srcValue = 0;
                    }


                    if (CheckComparison(srcValue, instruction.ComparisonOperator, instruction.ComparisonValue))
                    {
                        int destValue;
                        if (!registers.TryGetValue(instruction.DestinationRegister, out destValue))
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
            return (registers,maxValue);
        }

        private int IncrementValue(int destValue, int incrementAmount, bool incrementDestination)
        {
            return incrementDestination
                ? destValue + incrementAmount
                : destValue - incrementAmount;
        }

        private bool CheckComparison(int srcValue, string op, int compareValue)
        {
            switch (op)
            {
                case ">":
                    return srcValue > compareValue;

                case "<":
                    return srcValue < compareValue;

                case ">=":
                    return srcValue >= compareValue;

                case "<=":
                    return srcValue <= compareValue;

                case "==":
                    return srcValue == compareValue;

                case "!=":
                    return srcValue != compareValue;
            }

            throw new ArgumentException($"Unknown comparison operator {op}");
        }

        private class Instruction
        {
            private static Regex _regex = new Regex("^(?<dest>[a-z]+) (?<inc>inc|dec) (?<amount>[-\\d]+) if (?<src>[a-z]+) (?<op>>|<|>=|<=|==|!=) (?<comp>[-\\d]+)$");

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
                switch (value)
                {
                    case "inc":
                        return true;

                    case "dec":
                        return false;
                }

                throw new ArgumentException($"Unknown increment value \"{value}\"");
            }
        }
    }
}
