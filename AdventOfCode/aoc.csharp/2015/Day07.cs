using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace aoc.csharp._2015
{
    public class Day07 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            List<Gate> part1Inputs = ParseInput(input);
            Dictionary<string, ushort> wires = GetFinal(part1Inputs);

            ushort part1 = wires["a"];

            List<Gate> part2Inputs = new(part1Inputs.Count);
            for (int i = 0; i < part1Inputs.Count; i++)
            {
                var gate = part1Inputs[i];
                if (gate.outWire == "b")
                {
                    gate = new Gate(GateType.Value, part1.ToString(), "", "b");
                }

                part2Inputs.Add(gate);
            }

            wires = GetFinal(part2Inputs);
            ushort part2 = wires["a"];

            return (part1.ToString(), part2.ToString());
        }

        public static Dictionary<string, ushort> GetFinal(List<Gate> gates)
        {
            gates = new List<Gate>(gates);
            Dictionary<string, ushort> wires = new();

            bool TryGetValue(string input, out ushort value)
            {
                if (ushort.TryParse(input, out value))
                {
                    return true;
                }
                else if (wires.TryGetValue(input, out value))
                {
                    return true;
                }

                return false;
            }

            while (gates.Count > 0)
            {
                bool progressed = false;
                for (int i = gates.Count - 1; i >= 0; i--)
                {
                    Gate gate = gates[i];
                    ushort? value = null;
                    switch (gate.Type)
                    {
                        case GateType.Value:
                            {
                                if (TryGetValue(gate.inWire1, out ushort parsedValue))
                                {
                                    value = parsedValue;
                                }
                            }
                            break;

                        case GateType.And:
                        case GateType.Or:
                            {
                                if (TryGetValue(gate.inWire1, out ushort value1)
                                    && TryGetValue(gate.inWire2, out ushort value2))
                                {
                                    value = gate.Type == GateType.And
                                        ? (ushort)(value1 & value2)
                                        : (ushort)(value1 | value2);
                                }
                            }
                            break;

                        case GateType.LShift:
                        case GateType.RShift:
                            {
                                if (TryGetValue(gate.inWire1, out ushort inValue))
                                {
                                    ushort bits = ushort.Parse(gate.inWire2);
                                    value = gate.Type == GateType.LShift
                                        ? (ushort)(inValue << bits)
                                        : (ushort)(inValue >> bits);
                                }
                            }
                            break;

                        case GateType.Not:
                            {
                                if (TryGetValue(gate.inWire1, out ushort inValue))
                                {
                                    unchecked
                                    {
                                        value = (ushort)(~inValue);
                                    }
                                }
                            }
                            break;

                        default:
                            throw new Exception();
                    }

                    if (value.HasValue)
                    {
                        if (!wires.TryAdd(gate.outWire, value.Value))
                        {
                            throw new Exception();
                        }

                        gates.RemoveAt(i);
                        progressed = true;
                    }
                }


                if (!progressed)
                {
                    throw new Exception("Made a pass with no progress");
                }
            }

            return wires;
        }

        public static List<Gate> ParseInput(TextReader input)
        {
            Regex valueGateRegex = new(@"^(?<op1>\d+|\w+) -> (?<outWire>\w+)$");
            Regex andGateRegex = new(@"^(?<op1>\w+) AND (?<op2>\w+) -> (?<outWire>\w+)$");
            Regex orGateRegex = new(@"^(?<op1>\w+) OR (?<op2>\w+) -> (?<outWire>\w+)$");
            Regex lShiftRegex = new(@"^(?<op1>\w+) LSHIFT (?<op2>\d+) -> (?<outWire>\w+)$");
            Regex rShiftRegex = new(@"^(?<op1>\w+) RSHIFT (?<op2>\d+) -> (?<outWire>\w+)$");
            Regex notRegex = new(@"^NOT (?<op1>\w+) -> (?<outWire>\w+)$");

            List<Gate> gates = new();
            string? line;
            while ((line = input.ReadLine()) != null)
            {
                Match match;
                GateType gateType;
                if ((match = valueGateRegex.Match(line)).Success)
                {
                    gateType = GateType.Value;
                }
                else if ((match = andGateRegex.Match(line)).Success)
                {
                    gateType = GateType.And;
                }
                else if ((match = orGateRegex.Match(line)).Success)
                {
                    gateType = GateType.Or;
                }
                else if ((match = lShiftRegex.Match(line)).Success)
                {
                    gateType = GateType.LShift;
                }
                else if ((match = rShiftRegex.Match(line)).Success)
                {
                    gateType = GateType.RShift;
                }
                else if ((match = notRegex.Match(line)).Success)
                {
                    gateType = GateType.Not;
                }
                else
                {
                    throw new Exception("Couldn't parse line: " + line);
                }

                string op1 = match.Groups["op1"].Value;
                string op2 = match.Groups["op2"].Value;
                string outWire = match.Groups["outWire"].Value;
                Gate gate = new Gate(gateType, op1, op2, outWire);

                gates.Add(gate);
            }

            return gates;
        }

        public record Gate(GateType Type, string inWire1, string inWire2, string outWire);

        public enum GateType
        {
            Value,
            And,
            Or,
            LShift,
            RShift,
            Not
        }
    }
}
