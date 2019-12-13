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
            var memory = Input.To<int[]>(input);

            var vm = new IntcodeVm(memory);
            vm.Input.Enqueue(1);
            while (vm.Step()) ;

            int? outputValue = null;
            while (vm.Output.TryDequeue(out var value))
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

            vm = new IntcodeVm(memory);
            vm.Input.Enqueue(5);
            while (vm.Step()) ;

            if (vm.Output.Count != 1)
            {
                throw new Exception("Part 2 expected 1 output value, but got " + vm.Output.Count);
            }
            var part2 = vm.Output.Dequeue().ToString();

            return (part1, part2);
        }
    }
}
