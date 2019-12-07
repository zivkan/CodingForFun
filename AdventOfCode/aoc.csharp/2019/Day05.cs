using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2019
{
    public class Day05 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var vmInput = new Queue<int>();
            vmInput.Enqueue(1);
            var memory = Input.To<int[]>(input);
            var workingMemory = (int[])memory.Clone();

            var vmOutput = IntcodeVm.RunProgram(workingMemory, vmInput);

            int? outputValue = null;
            while (vmOutput.TryDequeue(out var value))
            {
                if (outputValue.HasValue)
                {
                    if (outputValue != 0)
                    {
                        throw new Exception("Diagnostic value == " + outputValue.Value);
                    }
                }

                outputValue = value;
            }

            string part1 = outputValue?.ToString() ?? throw new Exception("no output");

            vmInput.Clear();
            vmInput.Enqueue(5);
            vmOutput = IntcodeVm.RunProgram(memory, vmInput);
            if (vmOutput.Count != 1)
            {
                throw new Exception("Part 2 expected 1 output value, but got " + vmOutput.Count);
            }
            var part2 = vmOutput.Dequeue().ToString();

            return (part1, part2);
        }
    }
}
